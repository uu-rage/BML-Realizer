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
    /// Compound behavior to specify the timing and alignment of several (partial) face expressions as one unit.
    /// </summary>
    public class BMLFace : BMLBehavior
    {
        /// <summary>
        /// A float value between 0..1 to indicate the amount to which the expression should be shown on the face, 0 meaning 'not at all’ and 1 meaning 'maximum, highly exaggerated’
        /// </summary>
        public float amount;

        /// <summary>
        /// Fraction of overshoot of the attack peak, relative to amount (which defines the level of the sustain phase)
        /// </summary>
        public float overshoot;

        /// <summary>
        /// constructor
        /// </summary>
        public BMLFace()
        {
            amount = 0.5f;
            overshoot = 0.0f;
        }

        /// <summary>
        /// parsing the xml
        /// atribute: id, amount, overshoot
        /// synx attribute: start, attackPeak, relax, end
        /// </summary>
        /// <param name="reader"></param> XMLReader
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            amount = TryParseAtribute<float>(reader, "amount", 0.5f, false);
            overshoot = TryParseAtribute<float>(reader, "overshoot", 0.0f, false);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "attackPeak");
            TryParseSyncPoint(reader, "relax");
            TryParseSyncPoint(reader, "end");
        }
        
    }
}   