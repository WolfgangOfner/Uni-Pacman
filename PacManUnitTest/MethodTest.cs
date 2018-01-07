using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanClient;
using PacManLib;


namespace PacManUnitTest
{
    [TestClass]
    public class MethodTest
    {
        private Map map;
        private PacMan pacman;
        private Ghost ghost;

        [TestInitialize]
        public void init()
        {
            map = new Map();
            pacman = new PacMan(map);
            ghost = new Ghost(map, 15, 5);
        }

        [TestMethod]
        public void PacmanCanMove()
        {
            Assert.IsTrue(pacman.Left == 5);
            Assert.IsTrue(pacman.Top == 5);

            pacman.MoveRight();

            Assert.IsTrue(pacman.Left == 6);
            Assert.IsTrue(pacman.Top == 5);

            Assert.IsTrue(pacman.OldLeft == 5);
            Assert.IsTrue(pacman.OldTop == 5);
        }

        [TestMethod]
        public void HitWall()
        {
            Assert.IsTrue(pacman.Left == 5);
            Assert.IsTrue(pacman.Top == 5);

            if (GUI.CheckIfMoveIsValid(map.Maparray, pacman.Left - 1, pacman.Top))
            {
                pacman.MoveLeft();
            }

            Assert.IsTrue(pacman.Left == 5);
            Assert.IsTrue(pacman.Top == 5);
        }

        [TestMethod]
        public void CheckForSideChange()
        {
            pacman.Left = 1;
            pacman.Top = 32;

            Assert.IsTrue(pacman.Left == 1);
            Assert.IsTrue(pacman.Top == 32);

            Assert.IsTrue(pacman.CheckPacManPositionforSideChange(map, 1));
        }

        [TestMethod]
        public void SwitchPacmanToOtherSide()
        {
            pacman.Left = 1;
            pacman.Top = 32;

            Assert.IsTrue(pacman.Left == 1);
            Assert.IsTrue(pacman.Top == 32);

            pacman.SwitchPacManToOtherSide(map, - 1);

            Assert.IsTrue(pacman.Left == 81);
            Assert.IsTrue(pacman.Top == 32);
        }
        
        [TestMethod]
        public void ComplexMove()
        {
            pacman.Left = 29;
            pacman.Top = 26;

            for (int i = 0; i < 15; i++)
            {
                if (GUI.CheckIfMoveIsValid(map.Maparray, pacman.Left, pacman.Top + 1))
                {
                    pacman.MoveDown();    
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (GUI.CheckIfMoveIsValid(map.Maparray, pacman.Left + 1, pacman.Top))
                {
                    pacman.MoveRight();   
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (GUI.CheckIfMoveIsValid(map.Maparray, pacman.Left, pacman.Top + 1))
                {
                    pacman.MoveDown();
                }
            }

            for (int i = 0; i < 12; i++)
            {
                if (GUI.CheckIfMoveIsValid(map.Maparray, pacman.Left - 1, pacman.Top))
                {
                    pacman.MoveLeft();
                }
            }

            for (int i = 0; i < 15; i++)
            {
                if (GUI.CheckIfMoveIsValid(map.Maparray, pacman.Left, pacman.Top - 1))
                {
                    pacman.MoveUp();
                }
            }

            for (int i = 0; i < 19; i++)
            {
                if (GUI.CheckIfMoveIsValid(map.Maparray, pacman.Left - 1, pacman.Top))
                {
                    pacman.MoveLeft();
                }
            }

            if (pacman.CheckPacManPositionforSideChange(map, 1))
            {
                pacman.SwitchPacManToOtherSide(map, - 1);
            }

            Assert.IsTrue(pacman.Left == 81);
            Assert.IsTrue(pacman.Top == 32);
        }
    }
}
