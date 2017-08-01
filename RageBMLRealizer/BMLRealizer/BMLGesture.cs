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
    /// Currently, BML offers two types of gesture behaviors. The first provides a set of gestures recalled by name from a gesticon; the second provides simple pointing gestures.
    /// Coordinated movement with arms and hands, recalled from a gesticon by requesting the corresponding lexeme
    /// </summary>
    public class BMLGesture : BMLBehavior
    {
        /// <summary>
        /// What hand/arm is being used
        /// </summary>
        public Mode mode;

        /// <summary>
        /// Refers to an animation or a controller to realize this particular gesture.
        /// </summary>
        public Lexeme lexeme;

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
        /// Refers to an animation or a controller to realize this particular gesture.
        /// </summary>
        public enum Lexeme
        {
            BEAT
        }

        /// <summary>
        /// constructor
        /// </summary>
        public BMLGesture()
        {

        }

        /// <summary>
        /// parsing the xml
        /// atribute: mode, lexeme
        /// sync point: start, ready, strokeStart, stroke, strokeEnd, relax, end
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            mode = TryParseAtribute<Mode>(reader, "mode", Mode.NONE, false);
            lexeme = TryParseAtribute<Lexeme>(reader, "lexeme", Lexeme.BEAT, true);

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