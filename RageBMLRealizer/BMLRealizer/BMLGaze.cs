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
    /// Temporarily directs the gaze of the character towards a target.
    /// This behavior causes the character to temporarily direct its gaze to the requested target. 
    /// The influence parameter is read as follows: EYE means 'use only the eyes’; HEAD means 'use only head and eyes to change the gaze direction’, etcetera.
    /// </summary>
    public class BMLGaze : BMLBehavior
    {
        /// <summary>
        /// A reference towards a target instance that represents the target direction of the gaze.
        /// </summary>
        public string target;

        /// <summary>
        /// Determines what parts of the body to move to effect the gaze direction.
        /// </summary>
        public Influence influence;

        /// <summary>
        /// Adds an angle degrees offset to gaze direction relative to the target in the direction specified in the offsetDirection
        /// </summary>
        public float offsetAngle;

        /// <summary>
        /// Direction of the offsetDirection angle
        /// </summary>
        public Direction offsetDirection;

        /// <summary>
        /// Determines what parts of the body to move to effect the gaze direction.
        /// </summary>
        public enum Influence
        {
            NONE,
            EYES,
            HEAD,
            SHOULDER,
            WAIST,
            WHOLE
        }

        /// <summary>
        /// Direction of the offsetDirection angle.
        /// </summary>
        public enum Direction
        {
            RIGHT,
            LEFT,
            UP,
            DOWN,
            UPRIGHT,
            UPLEFT,
            DOWNLEFT,
            DOWNRIGHT
        }

        /// <summary>
        /// constructor
        /// </summary>
        public BMLGaze()
        {
            offsetAngle = 0.0f;
            offsetDirection = Direction.RIGHT;
        }

        /// <summary>
        /// parsing the xml
        /// atribute: target, influence, offsetAngle, offsetDirection
        /// sync point: start, ready, relax, end
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            target = TryParseAtribute<string>(reader, "target", "", true);
            influence = TryParseAtribute<Influence>(reader, "influence", Influence.NONE, false);
            offsetAngle = TryParseAtribute<float>(reader, "offsetAngle", 0.0f, false);
            offsetDirection = TryParseAtribute<Direction>(reader, "offsetDirection", Direction.RIGHT, false);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "ready");
            TryParseSyncPoint(reader, "relax");
            TryParseSyncPoint(reader, "end");
        }

    }
}