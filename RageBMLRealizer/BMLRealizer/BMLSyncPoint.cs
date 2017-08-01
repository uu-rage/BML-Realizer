/*
  Copyright 2016 Utrecht University http://www.uu.nl/
   
  This software has been created in the context of the EU-funded RAGE project.
  Realising and Applied Gaming Eco-System (RAGE), Grant agreement No 644187,
  http://rageproject.eu/
 
  The Behavior Markup Language (BML) is a language whose specifications were developed
  in the SAIBA framework. More information here : http://www.mindmakers.org/projects/bml-1-0/wiki
 
  Created by: Christyowidiasmoro, Utrecht University <c.christyowidiasmoro@uu.nl>
 
  For more information, contact Dr. Zerrin YUMAK, Email: z.yumak@uu.nl Web: http://www.zerrinyumak.com/
  https://www.staff.science.uu.nl/~yumak001/UUVHC/index.html
*/

using System.Collections.Generic;

using AssetPackage;

namespace BMLRealizer
{
    /// <summary>
    /// BML Sync Point class
    /// possible format:
    /// behavior_id:sync_id [+/- offset]
    /// A reference to a sync point of another behavior, optionally with a float offset in seconds. 
    /// By default, this is a behavior in the same <bml> block that the syncref is contained in; 
    /// if optional prefix block_id: is present, the syncref specifies a sync point of a behavior in the <bml> block with that ID.)
    /// offset: A positive float offset in seconds relative to the start time of the surrounding <bml> block.
    /// 
    /// </summary>
    public class BMLSyncPoint
    {
        /// <summary>
        /// the block where this sync point is used
        /// </summary>
        private BMLBlock parentBlock;

        /// <summary>
        /// the event name of this sync point (start, ready, strokeStart, attackPeak, stroke, strokeEnd, relax, end)
        /// </summary>
        private string eventName;

        /// <summary>
        /// timer of this sync point. if this sync point have dependency, then timer will hold the offset value after the dependency is completed
        /// </summary>
        private float timer = 0.0f;

        /// <summary>
        /// the ID of BML block which triggerBlockId is resided (optional)
        /// </summary>
        //private string triggerBMLId = "";

        /// <summary>
        /// the ID of dependency block
        /// </summary>
        private string triggerBlockId = "";

        /// <summary>
        /// the sync point name of the dependency block that we need to wait
        /// </summary>
        private string triggerEventName = "";

        /// <summary>
        /// flag whether this sync point is completed or not
        /// </summary>
        private bool completed;


        /// <summary>
        /// the contructor of BML Sync Point.
        /// </summary>
        /// <param name="eventName"></param> the name of sync point event (start, ready, strokeStart, attackPeak, stroke, strokeEnd, relax, end)
        /// <param name="value"></param> the atribute value that we need to parse.
        public BMLSyncPoint(BMLBlock parent, string eventName, string value)
        {
            parentBlock = parent;

            completed = false;

            this.eventName = eventName;

            if (value != null)
            {
                float offset = 0.0f;
                if (!float.TryParse(value, out offset))
                {
                    // split between behavior id and sync id
                    string[] offsetSplit = value.Split(':');
                    if (offsetSplit.Length > 1)
                    {
                        /*
                        // is contain BML id ?
                        if (offsetSplit.Length == 3)
                        {
                            triggerBMLId = offsetSplit[0];                            
                        }
                        */

                        triggerBlockId = offsetSplit[offsetSplit.Length - 2];
                        // split between sync id and offset
                        string[] offsetSplit2 = offsetSplit[offsetSplit.Length - 1].Split('+');
                        if (offsetSplit2.Length > 1)
                        {
                            // sync id
                            triggerEventName = offsetSplit2[0];
                            // offset
                            offset = float.Parse(offsetSplit2[1]);
                        }
                        else
                        {
                            // only sync id without offset
                            triggerEventName = offsetSplit[offsetSplit.Length - 1];
                        }
                    }
                    else
                    {
                        // not known!
                    }
                }

                timer = offset;
            }
        }

        /// <summary>
        /// function that need to be called everytime the realizer update is called
        /// </summary>
        /// <param name="realizer"></param> The realizer
        public void Update(RageBMLRealizer bmlNet)
        {
            // do not need to check if this synpoint is already completed.
            // TODO busy waiting ?
            if (completed)
                return;

            // check if the timer is safe to used.
            if (IsTimerSafe(bmlNet.ScheduledBlocks, bmlNet.Timer))
            {
                if (bmlNet.Timer >= timer)
                {
                    // complete this syncpoint
                    TriggerSyncPoint();

                    // call the event callback
                    bmlNet.OnSyncPointCompleted(parentBlock.id, eventName);

                    // if the eventName is 'end' than this behavior completed.
                    if (eventName == "end")
                    {
                        // if all bml inside this bml are already finish
                        if (parentBlock.parentBml.IncreaseEndChild())
                        {
                            // trigger globalEnd. this sync point to be used as a flag for another BML (especially for APPEND composition)
                            bmlNet.TriggerSyncPoint(parentBlock.parentBml.id, "globalEnd");
                        }

                        // remove the block and all related sync point
                        //realizer.RemoveBlock(parentBlock);
                    }

                }
            }
        }

        /// <summary>
        /// trigger this syncpoint to complete
        /// </summary>
        /// <returns></returns>
        public bool TriggerSyncPoint()
        {
            if (!completed)
            {
                // TODO: need to check, which one is better. Only set complete variable or destroy the object ??
                completed = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// is this syncpoint already completed ?
        /// </summary>
        /// <returns></returns>
        public bool IsCompleted()
        {
            return completed;
        }

        /// <summary>
        /// function to check whether the timer variable is safe to used or not
        /// </summary>
        /// <param name="realizer"></param>
        /// <returns></returns>
        public bool IsTimerSafe(Dictionary<string, BMLBlock> blocks, float globalTimer)
        {
            // if this <bml> tag is already started?
            BMLSyncPoint globalStartSyncPoint;
            if (parentBlock.parentBml != null)
            {
                if (parentBlock.parentBml.syncPoints.TryGetValue("globalStart", out globalStartSyncPoint))
                {
                    if (globalStartSyncPoint.IsCompleted() == false)
                    {
                        // we need to wait the bml globalStart to start
                        return false;
                    }
                }
            }

            // TODO: trigger with BML id

            // retrieve block dependency
            if (triggerBlockId != "")
            {
                BMLBlock triggerBlock;
                if (blocks.TryGetValue(triggerBlockId, out triggerBlock))
                {
                    // retrieve sync point 
                    BMLSyncPoint triggerSyncPoint;
                    if (triggerBlock.syncPoints.TryGetValue(triggerEventName, out triggerSyncPoint))
                    {
                        // is the dependency syncpoint is completed?
                        if (triggerSyncPoint.IsCompleted())
                        {
                            // set timer with current realizer timer plus offset
                            timer += globalTimer;

                            // to prevent setting timer more than one
                            triggerBlockId = "";

                            // it is safe to use the timer variable, because the dependency is already completed
                            return true;
                        }
                        else
                        {
                            // it is NOT safe to use the timer variable, because this syncpoint DEPEND on another syncpoint
                            return false;
                        }
                    }
                    else
                    {
                        // the syncpoint is not exist. we need to call TriggerSyncPoint(behaviorID, eventName) to create this syncpoint
                        return false;
                    }
                }
                else
                {
                    // the block / behavior is not exist. we need to wait until this behavior exist. call CreateBehaviorFromString() to create this block / behavior
                    return false;
                }
            }
            else
            {
                // it is safe to use the timer variable, because this syncpoint do not depend on anything
                return true;
            }
        }
        
    }
}
