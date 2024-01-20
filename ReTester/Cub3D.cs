using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;
using System.IO;
using ReTester;

namespace ConsoleApp3
{
    public class Cub3D
    {
        public Vector3[] vertices = new Vector3[4];
        public TextureFromBMP texture;


        public float x, y, z, size;
        Color color;
        public Cub3D(float size, Color color)
        {
            x = y = z = 1;
            this.size = size;
            this.color = color;
        }

        public Cub3D()
        {
            x = y = z = 1;
            size = 1;
            this.color = Color.Gray;

        }

        public Cub3D(float x, float y, float z, float size, Color color)
        {
            this.color = color;

            this.x = x;
            this.y = y;
            this.z = z;
            this.size = size;
        }
        public Cub3D(float size, Color color, float x, float y, float z, TextureFromBMP texture)
        {
            this.color = color;
            this.size = size;
            this.x = x;
            this.y = y;
            this.z = z;
            this.texture = texture; 
        }
        private void SetCoordinates(int sideVal, float scaleVal, int _moveX, int _moveY, int _moveZ)
        {
            vertices[0] = new Vector3(0 + _moveX, 0 + _moveY, 0 + _moveZ);
            vertices[1] = new Vector3((int)(sideVal * scaleVal) + _moveX, 0 + _moveY, 0 + _moveZ);
            vertices[2] = new Vector3((int)(sideVal * scaleVal) + _moveX, 0 + _moveY, (int)(sideVal * scaleVal) + _moveZ);
            vertices[3] = new Vector3(0 + _moveX, 0 + _moveY, (int)(sideVal * scaleVal) + _moveZ);
        }
        public void setXYZ(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(color);
            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x - size, y + size, z - size);
            GL.Vertex3(x + size, y + size, z - size);
            GL.Vertex3(x + size, y - size, z - size);

            GL.Color3(color);
            GL.Vertex3(x + size, y - size, z - size);
            GL.Vertex3(x + size, y - size, z - size);
            GL.Vertex3(x + size, y - size, z + size);
            GL.Vertex3(x - size, y - size, z + size);

            GL.Color3(color);
            GL.Vertex3(x - size, y - size, z - size);
            GL.Vertex3(x - size, y - size, z + size);
            GL.Vertex3(x - size, y + size, z + size);
            GL.Vertex3(x - size, y + size, z - size);

            GL.Color3(color);
            GL.Vertex3(x - size, y - size, z + size);
            GL.Vertex3(x + size, y - size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x - size, y + size, z + size);

            GL.Color3(color);
            GL.Vertex3(x - size, y + size, z - size);
            GL.Vertex3(x - size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z - size);

            GL.Color3(color);
            GL.Vertex3(x + size, y - size, z - size);
            GL.Vertex3(x + size, y + size, z - size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y - size, z + size);

            GL.End();
        }
        public Vector3 Position { get; set; }




        public void UpdateOrbit(float orbitRadius, float orbitSpeed, float moveSpeed, float elapsedTime)
        {
            // Calculate the orbit position
            float angle = orbitSpeed * elapsedTime;
            float newX = orbitRadius * (float)Math.Cos(angle);
            float newZ = orbitRadius * (float)Math.Sin(angle);

            // Update the position along the orbit
            float moveDistance = moveSpeed * elapsedTime;
            x = newX + moveDistance;
            z = newZ;
        }
        public void UpdateOrbit(float orbitRadius, float orbitSpeed, float moveSpeed, float elapsedTime, double updatesCounter)
        {
            float angle = orbitSpeed * elapsedTime;
            float newX = orbitRadius * (float)Math.Cos(angle);
            float newZ = orbitRadius * (float)Math.Sin(angle);

            float moveDistance = moveSpeed * elapsedTime;
            x = newX + moveDistance;
            z = newZ;
        }
        public void UpdateOrbit(float orbitRadius, float orbitSpeed, float moveSpeed, float elapsedTime, ulong updatesCounter)
        {
            // Calculate the orbit position
            float angle = orbitSpeed * elapsedTime * updatesCounter; // Multiply by updatesCounter to ensure continuous movement
            float newX = orbitRadius * (float)Math.Cos(angle);
            float newZ = orbitRadius * (float)Math.Sin(angle);

            // Update the position along the orbit
            float moveDistance = moveSpeed * elapsedTime * updatesCounter; // Multiply by updatesCounter to ensure continuous movement
            x = newX + moveDistance;
            z = newZ;
        }





        public void miscare(Cub3D sunCube, Cub3D earth, Cub3D moon, double updatesCounter)
        {
            GL.PushMatrix();

            // Render the Sun
            sunCube.DrawCube();

            // Calculate the position of the Earth in its orbit around the Sun
            double scale = 0.1;
            double earthOrbitRadius = 120.0 * scale;
            double earthOrbitSpeed = 0.01; // Adjust the orbit speed as needed
            double earthOrbitAngle = earthOrbitSpeed * updatesCounter;

            double earthX = earthOrbitRadius * Math.Cos(earthOrbitAngle);
            double earthZ = earthOrbitRadius * Math.Sin(earthOrbitAngle);

            // Save the current matrix state for Earth
            GL.PushMatrix();

            // Translate to the position of the Earth around the Sun
            GL.Translate(earthX, 0, earthZ);

            // Render the Earth
            earth.DrawCube();

            // Calculate the position of the Moon in its orbit around the Earth
            double moonOrbitRadius = 30 * scale; // Adjusted orbit radius
            double moonOrbitSpeed = 0.02; // Adjust the orbit speed as needed
            double moonOrbitAngle = moonOrbitSpeed * updatesCounter;

            // Calculate the position of the Moon relative to the Earth
            double moonX = earthX + moonOrbitRadius * Math.Cos(moonOrbitAngle);
            double moonZ = earthZ + moonOrbitRadius * Math.Sin(moonOrbitAngle);

            // Save the current matrix state for the Moon
            GL.PushMatrix();

            // Translate to the position of the Moon around the Earth
            GL.Translate(moonX, 0, moonZ);

            // Render the Moon
            moon.DrawCube();

            // Restore the matrix state for the Moon
            GL.PopMatrix();

            // Restore the matrix state for Earth
            GL.PopMatrix();

            // Restore the matrix state for the Sun
            GL.PopMatrix();
        }

    }
}
