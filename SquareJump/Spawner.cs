using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareJump
{
    public class Spawner : Singleton<Spawner>
    {
        public bool isInitialized
        {
            get; private set;
        } = false;

        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private GameObject superJumpPlatformPrefab;
        private List<GameObject> platforms;

        private void Start()
        {
            platforms = new List<GameObject>();
        }

        public void Initialize(GameObject startPlatform)
        {
            if (isInitialized) return;

            platforms.Add(startPlatform);
            StartCoroutine(AddInitialPlatforms());
            isInitialized = true;
        }

        public void AddNewPlatform(bool all = true)
        {
            Vector2 newPosition = new Vector2(
                Random.Range(-5f, 5f),
                platforms[platforms.Count-1].transform.position.y + 4f + Random.Range(0f, 1f));
            GameObject instantiationPrefab = platformPrefab;
            if (all)
            {
                if(Random.Range(1, 7) < 3)
                    instantiationPrefab = superJumpPlatformPrefab;
            }
            GameObject newPlat = Instantiate(instantiationPrefab, newPosition, Quaternion.identity);
            platforms.Add(newPlat);
        }

        private IEnumerator AddInitialPlatforms()
        {
            for (int i = 0; i < 10; i++)
            {
                AddNewPlatform(false);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
