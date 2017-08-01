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

using System.Xml;

namespace BMLRealizer
{
    /// <summary>
    /// Deictic gesture towards the target specified by the target attribute
    /// </summary>
    public class BMLPointing : BMLBehavior
    {
        /// <summary>
        /// What hand/arm is being used
        /// </summary>
        public Mode mode;

        /// <summary>
        /// The gesture is directed towards this target
        /// </summary>
        public string target;
        
        /// <summary>
        /// What hand/arm is being used
        /// </summary>
        public enum Mode
        {
            NONE,
            LEFT_HAND,
            RIGHT_HAND,
            BOTH_HANDS
        }

        /// <summary>
        /// constructor
        /// </summary>
        public BMLPointing()
        {

        }

        /// <summary>
        /// parsing the xml
        /// atribute: id, target, mode
        /// sync point: start, ready, strokeStart, stroke, strokeEnd, relax, end
        /// </summary>
        /// <param name="reader"></param> XMLReader
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            mode = TryParseAtribute<Mode>(reader, "mode", Mode.NONE, false);
            target = TryParseAtribute<string>(reader, "target", "", true);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "ready");
            TryParseSyncPoint(reader, "stroke_start");
            TryParseSyncPoint(reader, "stroke");
            TryParseSyncPoint(reader, "stroke_end");
            TryParseSyncPoint(reader, "relax");
            TryParseSyncPoint(reader, "end");
        }
    }
}