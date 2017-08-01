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
    /// Show a (partial) face expression from a predefined lexicon.
    /// </summary>
    public class BMLFaceLexeme : BMLFace
    {

        /// <summary>
        /// 
        /// </summary>
        public Lexeme lexeme;

        /// <summary>
        /// 
        /// </summary>
        public enum Lexeme
        {
            NONE,
            OBLIQUE_BROWS,
            RAISE_BROWS,
            RAISE_LEFT_BROW,
            RAISE_RIGHT_BROW,
            LOWER_BROWS,
            LOWER_LEFT_BROW,
            LOWER_RIGHT_BROW,
            LOWER_MOUTH_CORNERS,
            LOWER_LEFT_MOUTH_CORNER,
            LOWER_RIGHT_MOUTH_CORNER,
            RAISE_MOUTH_CORNERS,
            RAISE_RIGHT_MOUTH_CORNER,
            RAISE_LEFT_MOUTH_CORNER,
            OPEN_MOUTH,
            OPEN_LIPS,
            WIDEN_EYES,
            CLOSE_EYES
        };

        /// <summary>
        /// constructor
        /// </summary>
        public BMLFaceLexeme()
        {

        }

        /// <summary>
        /// parsing the xml
        /// atribute: lexeme
        /// </summary>
        /// <param name="reader"></param>
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            lexeme = TryParseAtribute<Lexeme>(reader, "lexeme", Lexeme.NONE, true);
        }

    }
}