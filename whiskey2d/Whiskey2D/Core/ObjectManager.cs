﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{
    public class ObjectManager
    {
        private static ObjectManager instance;
        public static ObjectManager getInstance()
        {
            if (instance == null)
            {
                instance = new ObjectManager();
            }
            return instance;
        }

        private List<GameObject> gameObjects;

        private ObjectManager()
        {
           
        }

        /// <summary>
        /// Initializes the ObjectManager
        /// </summary>
        public void init()
        {
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Closes out the ObjectManager
        /// </summary>
        public void close()
        {

        }


        public void updateAll()
        {
            foreach (GameObject gob in gameObjects)
            {
                gob.update();
            }
        }

        public void addObject(GameObject gob)
        {
            gameObjects.Add(gob);
        }

        public void removeObject(GameObject gob)
        {
            gameObjects.Remove(gob);
        }

        //todo add a removeObject by ID

        public List<GameObject> getAllObjects() 
        {
            return gameObjects;
        }

        /// <summary>
        /// Get a list of all GameObjects that are of the specified type. 
        /// </summary>
        /// <typeparam name="G">The specific type of GameObject to search for</typeparam>
        /// <returns>A list of all GameObjects that are of the specified type</returns>
        public List<G> getAllObjectsOfType<G>() where G : GameObject
        {
            List<G> gobs = new List<G>();

            //TODO, make faster? maybe a map.
            gameObjects.ForEach((gob) =>
            {
                if (gob is G)
                {
                    gobs.Add((G)gob);
                }
            });


            return gobs;

        }

        public GameObject getObject(int id)
        {
            return null; //todo
        }

    }
}
