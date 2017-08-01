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

// <copyright file="RageBMLRealizer.cs" company="RAGE">
// Copyright (c) 2016 RAGE All rights reserved.
// </copyright>
// <author>Chris021</author>
// <date>12/5/2016 11:53:54 AM</date>
// <summary>Implements the RageBMLRealizer class</summary>
namespace AssetPackage
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;
    using System.IO;

    using AssetManagerPackage;
    using BMLRealizer;

    /// <summary>
    /// An BMLRealizer Rage asset
    /// </summary>
    public class RageBMLRealizer : BaseAsset
    {
        #region Fields

        /// <summary>
        /// callback function. it will be called when the specific sync point is completed
        /// </summary>
        /// <param name="id"></param> the ID of block
        /// <param name="eventName"></param>the event name of sync point (start, ready, strokeStart, attackPeak, stroke, strokeEnd, relax, end)
        public delegate void SyncPointCompleted(string id, string eventName);
        public SyncPointCompleted OnSyncPointCompleted;

        /// <summary>
        /// Options for controlling the operation.
        /// </summary>
        private RageBMLRealizerSettings settings = null;

        /// <summary>
        /// dictionary that will hold the value of all block / behavior
        /// </summary>
        private Dictionary<string, Type> blocks = new Dictionary<string, Type>();

        /// <summary>
        /// the dictionary that save all the blocks / behavior that need to be run
        /// </summary>
        private Dictionary<string, BMLBlock> scheduledBlocks = new Dictionary<string, BMLBlock>();

        /// <summary>
        /// global timer
        /// </summary>
        private float timer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RageBMLRealizer.Asset class.
        /// </summary>
        public RageBMLRealizer()
            : base()
        {
            //! Create Settings and let it's BaseSettings class assign Defaultvalues where it can.
            // 
            settings = new RageBMLRealizerSettings();


            blocks.Add("bml", typeof(BMLBml));

            blocks.Add("wait", typeof(BMLWait));
            // TODO <synchronize>, <before>

            blocks.Add("face", typeof(BMLFace));
            blocks.Add("faceFacs", typeof(BMLFaceFacs));
            blocks.Add("faceLexeme", typeof(BMLFaceLexeme));
            blocks.Add("faceShift", typeof(BMLFaceShift));

            blocks.Add("gaze", typeof(BMLGaze));
            blocks.Add("gazeShift", typeof(BMLGazeShift));

            blocks.Add("gesture", typeof(BMLGesture));
            blocks.Add("pointing", typeof(BMLPointing));

            blocks.Add("head", typeof(BMLHead));
            blocks.Add("headDirectionShift", typeof(BMLHeadDirectionShift));

            blocks.Add("locomotion", typeof(BMLLocomotion));

            blocks.Add("posture", typeof(BMLPosture));
            blocks.Add("postureShift", typeof(BMLPostureShift));
            blocks.Add("stance", typeof(BMLStance));
            blocks.Add("pose", typeof(BMLPose));

            blocks.Add("speech", typeof(BMLSpeech));

            // TODO <feedback> <blockProgress> 

            timer = 0.0f;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets options for controlling the operation.
        /// </summary>
        ///
        /// <remarks>   Besides the toXml() and fromXml() methods, we never use this property but use
        ///                it's correctly typed backing field 'settings' instead. </remarks>
        /// <remarks> This property should go into each asset having Settings of its own. </remarks>
        /// <remarks>   The actual class used should be derived from BaseAsset (and not directly from
        ///             ISetting). </remarks>
        ///
        /// <value>
        /// The settings.
        /// </value>
        public override ISettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = (value as RageBMLRealizerSettings);
            }
        }

        /// <summary>
        /// the dictionary that hold the blocks / behavior that need to be run
        /// </summary>
        public Dictionary<string, BMLBlock> ScheduledBlocks
        {
            get
            {
                return scheduledBlocks;
            }
        }

        /// <summary>
        /// global timer
        /// </summary>
        public float Timer
        {
            get
            {
                return timer;
            }
        }

        #endregion Properties

        #region Methods

        public void ParseFromFile(string filename)
        {
            AssetManager.Instance.Log(Severity.Warning, "not supported yet in portable version");
            //XmlTextReader reader = new XmlTextReader(filename);
            //if (reader != null)
            //    Parse(reader);
            //else
            //    AssetManager.Instance.Log(Severity.Warning, "file error");
        }

        public void ParseFromString(string xml)
        {
            XmlReader reader = XmlReader.Create(new StringReader(xml));

            Parse(reader);
        }

        /// <summary>
        /// update function will be called everytime when the program is run. it can be called inside Unity Update function
        /// </summary>
        /// <param name="deltaTime"></param> the time from last called
        public void Update(float deltaTime)
        {
            timer += deltaTime;

            foreach (KeyValuePair<string, BMLBlock> block in scheduledBlocks)
            {
                foreach (KeyValuePair<string, BMLSyncPoint> syncPoint in block.Value.syncPoints)
                {
                    syncPoint.Value.Update(this);
                }
            }
        }

        /// <summary>
        /// this function can be called from outside library to trigger sync point. 
        /// </summary>
        /// <param name="id"></param> the ID of the block where the sync point is resided
        /// <param name="eventName"></param> the event name of sync point (start, ready, strokeStart, attackPeak, stroke, strokeEnd, relax, end)
        public void TriggerSyncPoint(string id, string eventName)
        {
            if (scheduledBlocks.ContainsKey(id))
            {
                if (scheduledBlocks[id].syncPoints.ContainsKey(eventName) == false)
                {
                    // create a new sync point
                    scheduledBlocks[id].syncPoints.Add(eventName, new BMLSyncPoint(scheduledBlocks[id], eventName, ""));
                }

                // trigger sync point
                scheduledBlocks[id].syncPoints[eventName].TriggerSyncPoint();
            }
        }

        /// <summary>
        /// function to get behavior from ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BMLBlock GetBehaviorFromId(string id)
        {
            if (scheduledBlocks.ContainsKey(id))
            {
                return scheduledBlocks[id];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// parsing the XML 
        /// TODO: need to check whether we have bml tag or not
        /// </summary>
        /// <param name="reader"></param> the XMLReader
        private void Parse(XmlReader reader)
        {
            BMLBml currentBml = null;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (blocks.ContainsKey(reader.Name))
                        {
                            Type t = blocks[reader.Name];
                            BMLBlock instance = (BMLBlock)Activator.CreateInstance(t);
                            instance.Parse(reader);

                            // TODO: need to accomodate different tag such as constraint, ...
                            if (instance is BMLBml)
                            {
                                BMLBml bml = (BMLBml)instance;

                                if (bml.composition == BMLBml.Composition.REPLACE)
                                {
                                    // The new block will completely replace all prior <bml> blocks. 
                                    // All behavior specified in earlier blocks will be ended and 
                                    ClearBlocks();

                                    // TODO: the ECA will revert to a neutral state before the new block starts.
                                }
                                else if (bml.composition == BMLBml.Composition.APPEND)
                                {
                                    // The start time of the new block will be as soon as possible after the end time of all prior blocks.
                                    if (currentBml != null)
                                        bml.SetGlobalStartTrigger(currentBml.id + ":globalEnd");
                                }
                                else if (bml.composition == BMLBml.Composition.MERGE)
                                {
                                    // The behaviors specified in the new <bml> block will be realized together with the behaviors specified in prior <bml> blocks. 
                                    // TODO: In case of conflict, behaviors in the newly merged <bml> block cannot modify behaviors defined by prior <bml> blocks.
                                }

                                // save this bml for upcomming tag
                                currentBml = bml;

                                // add to scheduled block
                                scheduledBlocks.Add((bml).id, bml);
                            }
                            else
                            {
                                // create a new bml if this xml is started without <bml> tag
                                if (currentBml == null)
                                {
                                    currentBml = new BMLBml();
                                }
                                // track this tag belong to a bml tag
                                instance.parentBml = currentBml;

                                // keep tracking the number of tag inside <bml> tag. in order to check whether all bml inside this <bml> tag are already finish or not
                                instance.parentBml.IncreaseChild();

                                if (instance is BMLBehavior)
                                {
                                    // add to scheduled block
                                    scheduledBlocks.Add(((BMLBehavior)instance).id, (BMLBehavior)instance);
                                }
                            }
                        }
                        break;
                }

            }
        }

        private void ClearBlocks()
        {
            IEnumerator enumerator = scheduledBlocks.GetEnumerator();

            while (enumerator.MoveNext())
            {
                //get the pair of Dictionary
                KeyValuePair<string, BMLBlock> pair = ((KeyValuePair<string, BMLBlock>)(enumerator.Current));

                //dispose it
                pair.Value.Dispose();
            }

            // clear the dictionary
            scheduledBlocks.Clear();
        }

        #endregion Methods
    }
}