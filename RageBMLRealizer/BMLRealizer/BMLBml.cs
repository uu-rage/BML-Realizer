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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BMLRealizer
{
    public class BMLBml : BMLBlock
    {

        /// <summary>
        /// a reference towards the controlled character
        /// </summary>
        public string characterId;

        public string xmlns;

        /// <summary>
        /// one among [MERGE,APPEND,REPLACE], defines the composition policy to apply if the current <bml> block overlaps with previous <bml> blocks (see below).
        /// </summary>
        public Composition composition;

        private int childNumber;

        private int endChildNumber;

        /// <summary>
        /// one among [MERGE,APPEND,REPLACE], defines the composition policy to apply if the current <bml> block overlaps with previous <bml> blocks (see below).
        /// </summary>
        public enum Composition
        {
            MERGE,
            APPEND,
            REPLACE
        }

        /// <summary>
        /// constructor
        /// </summary>
        public BMLBml()
        {
            parentBml = null;

            childNumber = 0;
            endChildNumber = 0;

            id = DateTime.Now.Subtract(new DateTime(1970, 1, 9, 0, 0, 00)).TotalMilliseconds.ToString();
            characterId = "";
            composition = Composition.MERGE;
        }

        /// <summary>
        /// parsing the xml
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            id = TryParseAtribute<string>(reader, "id", "", true);
            xmlns = TryParseAtribute<string>(reader, "xmlns", "http://www.bml-initiative.org/bml/bml-1.0", false);
            characterId = TryParseAtribute<string>(reader, "characterId", "", false);
            composition = TryParseAtribute<Composition>(reader, "composition", Composition.MERGE, false);

            // The start time of the new <bml> block will be as soon as possible. 
            SetGlobalStartTrigger("0");
        }

        public void SetGlobalStartTrigger(string value)
        {
            if (syncPoints.ContainsKey("globalStart"))
            {
                syncPoints["globalStart"] = new BMLSyncPoint(this, "globalStart", value);
            }
            else
            {
                syncPoints.Add("globalStart", new BMLSyncPoint(this, "globalStart", value));
            }
        }

        public void IncreaseChild()
        {
            childNumber++;
        }

        public bool IncreaseEndChild()
        {
            endChildNumber++;

            return (endChildNumber == childNumber);
        }
    }
}
