using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nsCard;
using System.Resources;


public class Class1
{
	public Class1()
    {
    }// end constructor

    // ***************************************************** //
    // ************  DRAG AND DROP EXAMPLE ***************** //
    // ***************************************************** //
    // Changing the cursor during drag-n-drop:
    // http://stackoverflow.com/questions/6175408/is-it-possible-to-change-mouse-cursor-when-handling-drag-from-dragover-event
    // from here: http://www.codeproject.com/Articles/3760/How-to-implement-simple-drag-and-drop-functionalit
    // don't forget: the control that receives the drag and drop has to have AllowDrop set to true.
    public void MainGame()
    {
        // don't forget: the control that receives the drag and drop has to have AllowDrop set to true.
        pictureBox1.AllowDrop = true;
    }
    private void imgPlayer1Deck05_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox sendingPictureBox = (PictureBox)sender;
        //sendingPictureBox.DoDragDrop((string)sendingPictureBox.ImageLocation, DragDropEffects.Copy);
        //sendingPictureBox.DoDragDrop((Bitmap)sendingPictureBox.Image, DragDropEffects.All);
        sendingPictureBox.DoDragDrop((string)sendingPictureBox.Image.ToString(), DragDropEffects.Copy);
    }
    private void pictureBox1_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text))
        //if (e.Data.GetDataPresent(DataFormats.Bitmap))
        {
            e.Effect = DragDropEffects.All;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }
    private void pictureBox1_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text))
        //if (e.Data.GetDataPresent(DataFormats.Bitmap))
        {
            /*Bitmap newImage = new Bitmap((Bitmap)e.Data.GetData(DataFormats.Bitmap));
            pictureBox1.Image = newImage;*/
                
            /*string newImageLocation = (string)e.Data.GetData(DataFormats.Text);
            pictureBox1.ImageLocation = newImageLocation;*/

            string newImageName = (string)e.Data.GetData(DataFormats.Text);
            ResourceManager resourceManager = Resource1.ResourceManager;
            pictureBox1.Image = (Bitmap)resourceManager.GetObject(newImageName);

            pictureBox1.Refresh();
        }
    }
    // ***************************************************** //
    // ************  END DRAG AND DROP EXAMPLE ***************** //
    // ***************************************************** //

    // ***************************************************** //
    // ************  bitmap tests ***************** //
    // ***************************************************** //
    private void button1_Click(object sender, EventArgs e)
    {
        //DisplayLineRegion thing = new DisplayLineRegion();
        //thing.GetRedImage();
        //allHands.BitmapTest();
        /*allHands.TestDisplayLines(iTestCount);
        DisplayAllDeckContents();
        iTestCount++;
        if (iTestCount == 18)
        {
            iTestCount = 1;
        }*/
        bStop = true;
    }
    public void BitmapTest()
    {
        for (int iStripeCount = 1; iStripeCount <= 100; iStripeCount++)
        {
            for (int iHeightCount = 1; iHeightCount <= 300; iHeightCount++)
            {
                bmpDisplayLines.SetPixel(iStripeCount, iHeightCount, Color.FromArgb(255, 255, 255));
            }
        }
        for (int iStripeCount = 101; iStripeCount <= 200; iStripeCount++)
        {
            for (int iHeightCount = 1; iHeightCount <= 300; iHeightCount++)
            {            
                bmpDisplayLines.SetPixel(iStripeCount, iHeightCount, Color.FromArgb(0, 126, 255));
            }
        }
        for (int iStripeCount = 201; iStripeCount <= 300; iStripeCount++)
        {
            for (int iHeightCount = 1; iHeightCount <= 300; iHeightCount++)
            {
                bmpDisplayLines.SetPixel(iStripeCount, iHeightCount, Color.FromArgb(255, 0, 0));
            }
        }
        for (int iStripeCount = 301; iStripeCount <= 400; iStripeCount++)
        {
            for (int iHeightCount = 1; iHeightCount <= 300; iHeightCount++)
            {
                bmpDisplayLines.SetPixel(iStripeCount, iHeightCount, Color.FromArgb(0, 126, 126));
            }
        }
        for (int iStripeCount = 401; iStripeCount <= 500; iStripeCount++)
        {
            for (int iHeightCount = 1; iHeightCount <= 300; iHeightCount++)
            {
                bmpDisplayLines.SetPixel(iStripeCount, iHeightCount, Color.FromArgb(0, 0, 255));
            }
        }
        for (int iStripeCount = 501; iStripeCount <= 550; iStripeCount++)
        {
            for (int iHeightCount = 1; iHeightCount <= 300; iHeightCount++)
            {
                bmpDisplayLines.SetPixel(iStripeCount, iHeightCount, Color.FromArgb(0, 255, 0));
            }
        }

        bmpDisplayLines.Save("c:\\_working\\mytestimage.bmp");
    }
    // ***************************************************** //
    // ************  END bitmap tests ***************** //
    // ***************************************************** //


    // ***************************************************** //
    // ************  bitmap manipulation. SetPixel etc ***************** //
    // ***************************************************** // 
    // from here: From here: http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/ad60ec35-fe58-4d76-aa91-8da57e3abeff/
    public void GetRedImage()
    {
        using (Bitmap sourceImage = new Bitmap("c:\\sourceimage.bmp"))
        {
            using (Bitmap redBmp = new Bitmap(sourceImage.Width, sourceImage.Height))
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    for (int y = 0; y < sourceImage.Height; y++)
                    {
                        Color pxl = sourceImage.GetPixel(x, y);
                        Color redPxl = Color.FromArgb(pxl.R, 0, 0);

                        redBmp.SetPixel(x, y, redPxl);
                    }
                }

                redBmp.Save("c:\\myredimage.bmp");
            }
        }
    }
    // ***************************************************** //
    // ************  end bitmap manipulation. SetPixel etc ***************** //
    // ***************************************************** //    

    // ***************************************************** //
    // ************  lock bitmap bits ***************** //
    // ***************************************************** //    
    public void Test(ref Bitmap bmpDisplayLines)
    {
        // Lock the bitmap's bits.  
        Rectangle rect = new Rectangle(0, 0, 285, 445);
        System.Drawing.Imaging.BitmapData bmpData =
            bmpDisplayLines.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            System.Drawing.Imaging.PixelFormat.Format4bppIndexed);

        // Get the address of the first line.
        IntPtr ptr = bmpData.Scan0;

        // Declare an array to hold the bytes of the bitmap.
        int bytes = Math.Abs(bmpData.Stride) * bmpDisplayLines.Height;
        byte[] rgbValues = new byte[bytes];

        // Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        // Set every third value to 255. A 24bpp bitmap will look red.  
        for (int counter = 2; counter < rgbValues.Length; counter += 3)
            rgbValues[counter] = 255;

        // Copy the RGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

        // Unlock the bits.
        // displayLines.UnlockBits(bmpData);

        // Draw the modified image.
        // from here: http://msdn.microsoft.com/en-us/library/5ey6h79d.aspx
        // To run this example, paste it into a form and handle the form's Paint event by calling the LockUnlockBitsExample method, passing e as PaintEventArgs.
        // private void LockUnlockBitsExample(PaintEventArgs e)
        // e.Graphics.DrawImage(bmpDisplayLines, 0, 150);
    }
    // ***************************************************** //
    // ************  end lock bitmap bits ***************** //
    // ***************************************************** //    

    // ***************************************************** //
    // ************  draw lines, inc at angles ***************** //
    // ***************************************************** //    
    // from here: http://msdn.microsoft.com/en-us/library/dd0c4s09.aspx
    public void DrawPolygonPoint(PaintEventArgs e)
    {
        // Create pen.
        Pen blackPen = new Pen(Color.Black, 3);

        // Create points that define polygon.
        Point point1 = new Point(50,  50);
        Point point2 = new Point(100,  25);
        Point point3 = new Point(200,   5);
        Point point4 = new Point(250,  50);
        Point point5 = new Point(300, 100);
        Point point6 = new Point(350, 200);
        Point point7 = new Point(250, 250);
        Point[] curvePoints =
                    {
                        point1,
                        point2,
                        point3,
                        point4,
                        point5,
                        point6,
                        point7
                    };

        // Draw polygon to screen.
        e.Graphics.DrawPolygon(blackPen, curvePoints);
    }// end function
    // from here: http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/98494ffc-0a92-4d07-a93a-de28c3af2099
    private void Form1_Paint(object sender, PaintEventArgs e)
    {
        Bitmap BackgroundDraw = new Bitmap(this.Width, this.Height);
        Graphics BackgroundGraphics = Graphics.FromImage(BackgroundDraw);
        Graphics FormGraphics = this.CreateGraphics();

        //create a recrtangle which will be the source for the brush
        //we create it with the same size as the pen we are going to us
        //if it rectangle was smaler the image is replaciate over the 
        //pen to fill the remaining pixels. The converse happens if the
        //rectangle is bigger than the pen
        Rectangle rectBrush = new Rectangle(0, 0, 5, 5);
        LinearGradientBrush brush = new LinearGradientBrush(rectBrush, Color.White, Color.Black,
                        LinearGradientMode.Horizontal);

        //setup our blending options to get the effect we need
        Blend BlendOptions = new Blend();
        BlendOptions.Factors = new float[] { .5f, .85f, 1f, .85f, .50f, .14f, .0f, .14f, .49f };
        BlendOptions.Positions = new float[] { 0.0f, .125f, .25f, .375f, .5f, .625f, .75f, .875f, 1.0f };

        brush.Blend = BlendOptions;

        Pen p = new Pen(brush, 5);
        BackgroundGraphics.SmoothingMode = SmoothingMode.HighQuality;
        BackgroundGraphics.DrawLine(p, new Point(10, 10), new Point(10, 200));

        //create the secoond line
        brush = new LinearGradientBrush(rectBrush, Color.White, Color.Black, -38);
        brush.Blend = BlendOptions;
        p = new Pen(brush, 5);
        BackgroundGraphics.DrawLine(p, new Point(50, 10), new Point(200, 200));

        //create the third line.. 131 is the angle between the two points
        brush = new LinearGradientBrush(rectBrush, Color.White, Color.Black, 131);
        brush.Blend = BlendOptions;
        p = new Pen(brush, 5);
        BackgroundGraphics.DrawLine(p, new Point(80, 10), new Point(300, 200));
            
        //Draw or image onto the actual form
        FormGraphics.DrawImage(BackgroundDraw, 0, 0);
            
        //Clean up after ourselves
        BackgroundDraw.Dispose();
        BackgroundGraphics.Dispose();
        FormGraphics.Dispose();
        brush.Dispose();
        p.Dispose();
    }
    // ***************************************************** //
    // ************  end draw lines ***************** //
    // ***************************************************** //    

    // ***************************************************** //
    // ************  region data ***************** //
    // ***************************************************** //    
    // from here: http://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.regiondata.aspx
    // The following example is designed for use with Windows Forms, and it requires PaintEventArgs e, which is a parameter of the Paint event handler. The code performs the following actions:
    // Creates a rectangle and draw its to the screen in black.
    // Creates a region using the rectangle.
    // Gets the RegionData.
    // Draws the region data (an array of bytes) to the screen, by using the DisplayRegionData helper function.
    public void GetRegionDataExample(PaintEventArgs e)
    {

        // Create a rectangle and draw it to the screen in black.
        Rectangle regionRect = new Rectangle(20, 20, 100, 100);
        e.Graphics.DrawRectangle(Pens.Black, regionRect);

        // Create a region using the first rectangle.
        Region myRegion = new Region(regionRect);

        // Get the RegionData for this region.
        RegionData myRegionData = myRegion.GetRegionData();
        int myRegionDataLength = myRegionData.Data.Length;
        DisplayRegionData(e, myRegionDataLength, myRegionData);
    }
    // THIS IS A HELPER FUNCTION FOR GetRegionData.
    public void DisplayRegionData(PaintEventArgs e,
        int len,
        RegionData dat)
    {

        // Display the result.
        int i;
        float x = 20, y = 140;
        Font myFont = new Font("Arial", 8);
        SolidBrush myBrush = new SolidBrush(Color.Black);
        e.Graphics.DrawString("myRegionData = ",
            myFont,
            myBrush,
            new PointF(x, y));
        y = 160;
        for (i = 0; i < len; i++)
        {
            if (x > 300)
            {
                y += 20;
                x = 20;
            }
            e.Graphics.DrawString(dat.Data[i].ToString(),
                myFont,
                myBrush,
                new PointF(x, y));
            x += 30;
        }
    }
    // ***************************************************** //
    // ************  end region data ***************** //
    // ***************************************************** //  
  
    // ***************************************************** //
    // ************  Drawing cardinal splines ***************** //
    // ***************************************************** //  
    // from here: http://msdn.microsoft.com/en-us/library/ms533934%28v=vs.85%29.aspx
    void function()
    {
        // A cardinal spline is a curve that passes smoothly through a given set of points. To draw a cardinal spline, create a Graphics object and pass the address of an array of points to the Graphics::DrawCurve method. 
        // The following example draws a bell-shaped cardinal spline that passes through five designated points:
        Point points[] = {Point(0, 100),
        Point(50, 80),
        Point(100, 20),
        Point(150, 80),
        Point(200, 100)};

        Pen pen(Color(255, 0, 0, 255));
        graphics.DrawCurve(&pen, points, 5);

        // You can use the Graphics::DrawClosedCurve method of the Graphics class to draw a closed cardinal spline. 
        // In a closed cardinal spline, the curve continues through the last point in the array and connects with the first point in the array.

        // The following example draws a closed cardinal spline that passes through six designated points.
        Point points[] = {Point(60, 60),
        Point(150, 80),
        Point(200, 40),
        Point(180, 120),
        Point(120, 100),
        Point(80, 160)};

        Pen pen(Color(255, 0, 0, 255));
        graphics.DrawClosedCurve(&pen, points, 6);

        // You can change the way a cardinal spline bends by passing a tension argument to the Graphics::DrawCurve method. 
        // The following example draws three cardinal splines that pass through the same set of points:
        Point points[] = {Point(20, 50),
        Point(100, 10),
        Point(200, 100),
        Point(300, 50),
        Point(400, 80)};

        Pen pen(Color(255, 0, 0, 255));
        graphics.DrawCurve(&pen, points, 5, 0.0f);  // tension 0.0
        graphics.DrawCurve(&pen, points, 5, 0.6f);  // tension 0.6
        graphics.DrawCurve(&pen, points, 5, 1.0f);  // tension 1.0
    }
    // ***************************************************** //
    // ************  end Drawing cardinal splines ***************** //
    // ***************************************************** //  

    // ***************************************************** //
    // ************  widening paths 
    //                  (ie drawing new outlines around things) ***************** //
    // ***************************************************** //  
    // from here: http://msdn.microsoft.com/en-us/library/dhdhbs59.aspx
    private void WidenExample(PaintEventArgs e)
    {

        // Create a path and add two ellipses.
        GraphicsPath myPath = new GraphicsPath();
        myPath.AddEllipse(0, 0, 100, 100);
        myPath.AddEllipse(100, 0, 100, 100);

        // Draw the original ellipses to the screen in black.
        e.Graphics.DrawPath(Pens.Black, myPath);

        // Widen the path.
        Pen widenPen = new Pen(Color.Black, 10);
        Matrix widenMatrix = new Matrix();
        widenMatrix.Translate(50, 50);
        myPath.Widen(widenPen, widenMatrix, 1.0f);

        // Draw the widened path to the screen in red.
        e.Graphics.FillPath(new SolidBrush(Color.Red), myPath);
    }
    // this does the same but doesn't fill in the widened path, and draws it around the original ellipses.
    private void WidenExample(PaintEventArgs e)
    {
        // Create a path and add two ellipses.
        GraphicsPath myPath = new GraphicsPath();
        myPath.AddEllipse(5, 5, 100, 100);
        myPath.AddEllipse(100, 5, 100, 100);

        // Draw the original ellipses to the screen in black.
        e.Graphics.DrawPath(Pens.Black, myPath);

        // Widen the path.
        Pen widenPen = new Pen(Color.Black, 10);
        Matrix widenMatrix = new Matrix();
        //widenMatrix.Translate(50, 50);
        myPath.Widen(widenPen, widenMatrix, 1.0f);

        // Draw the widened path to the screen in red.
        e.Graphics.DrawPath(Pens.Red, myPath);
    }
    // ***************************************************** //
    // ************  end widening paths ***************** //
    // ***************************************************** //  
    

    // ***************************************************** //
    // ************  drawing arcs ***************** //
    // ***************************************************** //  
    private void DrawMyArc()
    {
            // This illustrates all the different start angles.
            // Sweep angle refers to the proportion of 360 degrees, ie how much of the circumference is drawn. 
            // So 180 degrees is a semi-circle.
            // The rectangle would completely enclose the full ellipse, so a square gives a circle.
            Pen newPen = new Pen(Color.Black, 10);
            // DrawArc(pen, rectangle, startAngle, sweepAngle)
            e.Graphics.DrawArc(newPen, new Rectangle(10, 100, 90, 90), 0, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(10, 200, 90, 90), 30, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(10, 300, 90, 90), 60, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(10, 400, 90, 90), 90, 180);

            e.Graphics.DrawArc(newPen, new Rectangle(110, 100, 90, 90), 120, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(110, 200, 90, 90), 150, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(110, 300, 90, 90), 180, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(110, 400, 90, 90), 210, 180);

            e.Graphics.DrawArc(newPen, new Rectangle(210, 100, 90, 90), 240, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(210, 200, 90, 90), 270, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(210, 300, 90, 90), 300, 180);
            e.Graphics.DrawArc(newPen, new Rectangle(210, 400, 90, 90), 330, 180);

            // 360 start angle is the same as 0
            e.Graphics.DrawArc(newPen, new Rectangle(310, 100, 90, 90), 360, 180);
    }
    // ***************************************************** //
    // ************  end drawing arcs ***************** //
    // ***************************************************** //  
    
}// end class
