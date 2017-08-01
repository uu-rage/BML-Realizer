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

namespace BMLRealizer
{
    /// <summary>
    /// This behavior provides control of the face through single Action Units from the Facial Action Coding Scheme. It is an Core Extension, that is, not every BML Compliant Realizer has to implement this behavior, but if a Realizer offers FACS based face control, they should adhere to the specification of this <faceFacs> behavior
    /// </summary>
    public class BMLFaceFacs : BMLFace
    {
        /// <summary>
        /// The number of the FACS Action Unit to be displayed
        /// </summary>
        public int au;

        /// <summary>
        /// Which side of the face to display the action unit on. Possible values: [LEFT,RIGHT,BOTH] Note that for some Action Units, BOTH is the only possible value
        /// </summary>
        public Side side;

        /// <summary>
        /// 
        /// </summary>
        public enum Side
        {
            LEFT,
            RIGHT,
            BOTH
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BMLFaceFacs()
        {
            this.side = Side.BOTH;
        }

    }
}