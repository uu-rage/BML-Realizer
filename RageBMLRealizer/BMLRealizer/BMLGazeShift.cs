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
    /// Permanently change the gaze direction of the character towards a certain target.
    /// This behavior causes the character to direct its gaze to the requested target. This changes the default state of the ECA: after completing this behavior, the new target is the default gaze direction of the ECA. The influence parameter is read as follows: EYE means 'use only the eyes’; HEAD means 'use only head and eyes to change the gaze direction’, etcetera.
    /// </summary>
    public class BMLGazeShift : BMLGaze
    {
        /// <summary>
        /// constructor
        /// sync attribute: start, end
        /// </summary>
        public BMLGazeShift()
        {

        }

        /// <summary>
        /// parsing the xml
        /// attribute:
        /// sync point: start, end
        /// </summary>
        /// <param name="reader"></param> XMLReader
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "end");
        }
    }
}