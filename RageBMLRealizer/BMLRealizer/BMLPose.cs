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
    /// Child element of <posture> and <postureShift> behaviors, defines additions to the global body posture of the ECA.
    /// Child element of <posture> and <postureShift> behaviors, defines additions that modify the global body posture of the ECA. For each value of the part attribute, only one <pose> child is expected to be present. A BML Realizer may define any number of lexemes beyond the ones specified above.
    /// </summary>
    public class BMLPose : BMLBehavior
    {
        /// <summary>
        /// What part of the body is affected? Possible values are [ARMS, LEFT_ARM, RIGHT_ARM, LEGS, LEFT_LEG, RIGHT_LEG, HEAD, WHOLEBODY]
        /// </summary>
        public Part part;

        /// <summary>
        /// What configuration is set to the given part? Some possible values are [ARMS_AKIMBO, ARMS_CROSSED, ARMS_NEUTRAL, ARMS_OPEN, LEGS_CROSSED, LEGS_NEUTRAL, LEGS_OPEN, LEANING_FORWARD, LEANING_BACKWARD, ...]
        /// </summary>
        public Lexeme lexeme;

        /// <summary>
        /// What part of the body is affected? Possible values are [ARMS, LEFT_ARM, RIGHT_ARM, LEGS, LEFT_LEG, RIGHT_LEG, HEAD, WHOLEBODY]
        /// </summary>
        public enum Part
        {
            NONE,
            ARMS,
            LEFT_ARM,
            RIGHT_ARM,
            LEGS,
            LEFT_LEG,
            RIGHT_LEG,
            HEAD,
            WHOLEBODY
        }

        /// <summary>
        /// What configuration is set to the given part? Some possible values are [ARMS_AKIMBO, ARMS_CROSSED, ARMS_NEUTRAL, ARMS_OPEN, LEGS_CROSSED, LEGS_NEUTRAL, LEGS_OPEN, LEANING_FORWARD, LEANING_BACKWARD, ...]
        /// </summary>
        public enum Lexeme
        {
            NONE,
            ARMS_AKIMBO,
            ARMS_CROSSED,
            ARMS_NEUTRAL,
            ARMS_OPEN,
            LEGS_CROSSED,
            LEGS_NEUTRAL,
            LEGS_OPEN,
            LEANING_FORWARD,
            LEANING_BACKWARD
        }

        /// <summary>
        /// constructor
        /// </summary>
        public BMLPose()
        {

        }

        /// <summary>
        /// parsing xml
        /// atribute: part, lexeme
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            part = TryParseAtribute<Part>(reader, "part", Part.NONE, true);
            lexeme = TryParseAtribute<Lexeme>(reader, "lexeme", Lexeme.NONE, true);
        }
    }
}
