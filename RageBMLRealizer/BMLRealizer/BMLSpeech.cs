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
using System.Xml;

using AssetManagerPackage;

namespace BMLRealizer
{
    /// <summary>
    /// Utterance to be spoken by a character.
    /// Realization of the <speech> element generates both speech audio (or text) and speech movement, for example using a speech synthesizer and viseme morphing.
    /// The<speech> element requires a sub-element.This sub-element is a<text> element that contains the text to be spoken, with optionally embedded<sync> elements for alignment with other behaviors.
    /// </summary>
    public class BMLSpeech : BMLBehavior
    {
        /// <summary>
        /// the text that need to be spoken
        /// </summary>
        public string text;

        /// <summary>
        /// constructor
        /// </summary>
        public BMLSpeech()
        {

        }

        /// <summary>
        /// parsing the xml
        /// child node: text
        /// sync point: start, end
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);
            
            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "end");

            if (reader.ReadToDescendant("text"))
            {
                text = reader.ReadElementContentAsString();
            }
            else
            {
                AssetManager.Instance.Log(AssetPackage.Severity.Warning, "cannot read text node in speech block");
            }

        }
    }
}