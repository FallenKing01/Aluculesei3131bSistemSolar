using ConsoleApp3;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using ReTester._3DObj;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReTester
{
    public partial class MainForm : Form
    {
        float camDepth;

        int eyePosX, eyePosY, eyePosZ;

        Axes mainAx;
        Square sq1;
        SquareTextured sq2;
        SquareVBO sq3;
        TextureFromBMP textureSquare2, textureSquare3;
        Cub3D sunCube = new Cub3D(0, 20, 0, 5, Color.OrangeRed);
        Cub3D earth = new Cub3D(500, 20, 100, 5, Color.Blue);
        Cub3D randomPlanet = new Cub3D(20, 20, 45, 3, Color.WhiteSmoke);
        Cub3D moon = new Cub3D(20, 20, 45, 2, Color.WhiteSmoke);
        private ulong updatesCounter;


        private Timer solarSystemTimer;

        public MainForm()
        {
            InitializeComponent();
            solarSystemTimer = new Timer();
            solarSystemTimer.Tick += new EventHandler(solarSysClk);
            solarSystemTimer.Interval = 16; // Set the interval (in milliseconds) for the timer
            solarSystemTimer.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupDefaultValues();
            SetupWindowGUI();
            SetupSceneObjects();

            MainTimer.Start();
        }

        private void SetupDefaultValues()
        {
            camDepth = 1.04f;
            eyePosX = 100;
            eyePosY = 100;
            eyePosZ = 50;
        }

        private void SetupWindowGUI()
        {

        }

        private void SetupSceneObjects()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.Texture2D);

            textureSquare2 = ContentLoader.LoadTexture("Content/Textures/pebbles.jpg");
            textureSquare3 = ContentLoader.LoadTexture("Content/Textures/map.png");

            mainAx = new Axes();
            sq1 = new Square();
            sq2 = new SquareTextured(textureSquare2, 1.0f, 15, 35, 15);
            sq3 = new SquareVBO(textureSquare3, 1.75f, 15, 72, 15);

            // Create Cub3D instances with textures
            Cub3D sunCube = new Cub3D(20, Color.OrangeRed, 20, 0, 10, textureSquare2);
            Cub3D earth = new Cub3D(25, Color.Blue, 20, 45, 5, textureSquare3);
            Cub3D moon = new Cub3D(20, Color.WhiteSmoke, 20, 45, 1, null); // You can set the moon's texture later

            Console.WriteLine("=============-----------------======================");
            Console.WriteLine("" + textureSquare2.ToString());
            Console.WriteLine("=============-----------------======================");
        }


        private void MainTimer_Tick(object sender, EventArgs e)
        {
            MainViewport.Invalidate();
            MainTimer.Stop();
        }

        private void BtnForceRefresh_Click(object sender, EventArgs e)
        {
            MainViewport.Invalidate();
        }

        private void BtnSquareNormal_Click(object sender, EventArgs e)
        {
            sq1.ToggleVisibility();

            MainViewport.Invalidate();
        }

        private void BtnSquareTextured_Click(object sender, EventArgs e)
        {
            sq2.ToggleVisibility();

            MainViewport.Invalidate();
        }

        private void BtnSquareVbo_Click(object sender, EventArgs e)
        {
            sq3.ToggleVisibility();

            MainViewport.Invalidate();
        }

        private void solarSysClk(object sender, EventArgs e)
        {
            updatesCounter++;

            // Update the position of the Sun
            sunCube.miscare(sunCube, earth, randomPlanet, updatesCounter);

            // Make the Earth orbit around the Sun and move slowly along the orbit
            float earthOrbitRadius = 120.0f; // Set the orbit radius for the Earth
            float earthOrbitSpeed = 0.01f;   // Set the orbit speed for the Earth
            float earthMoveSpeed = 0.005f;   // Set the movement speed along the orbit

            // Update the position of the Earth based on its orbit around the Sun
            earth.UpdateOrbit(earthOrbitRadius, earthOrbitSpeed, earthMoveSpeed, 0.0167f, updatesCounter);

            // Make the Moon orbit around the Earth
            float moonOrbitRadius = 30.0f; // Set the orbit radius for the Moon
            float moonOrbitSpeed = 0.02f;  // Set the orbit speed for the Moon

            // Update the position of the Moon based on its orbit around the Earth
            randomPlanet.UpdateOrbit(moonOrbitRadius, moonOrbitSpeed, 0.0167f, updatesCounter);

            // Make the Earth have a moon
            float earthMoonOrbitRadius = 45.0f; // Set the orbit radius for the Earth's moon
            float earthMoonOrbitSpeed = 0.03f;  // Set the orbit speed for the Earth's moon

            // Update the position of the Earth's moon based on its orbit around the Earth
            earth.UpdateOrbit(earthMoonOrbitRadius, earthMoonOrbitSpeed, 0.0167f, updatesCounter);

            MainViewport.Invalidate();
        }








        private void BtnSquaresReset_Click(object sender, EventArgs e)
        {
            sq1.SetInvisible();
            sq2.SetInvisible();
            sq3.SetInvisible();

            MainViewport.Invalidate();
        }

        private void MainViewport_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.Gray);

            SetView();

            // GRAPHICS PAYLOAD
            mainAx.DrawMe();
            sq1.DrawMe();        
            sq2.DrawMe();
            sq3.DrawMe();
            sunCube.DrawCube();
            randomPlanet.DrawCube();
            earth.DrawCube();

            MainViewport.SwapBuffers();
        }

        private void SetView() {
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(camDepth, 4 / 3, 1, 256);    
            Matrix4 lookat = Matrix4.LookAt(eyePosX, eyePosY, eyePosZ, 0, 0, 0, 0, 1, 0);                   
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);                                                             
            GL.LoadIdentity();
            GL.LoadMatrix(ref lookat);
            GL.Viewport(0, 0, MainViewport.Width, MainViewport.Height);                                        

        }

    }
}
