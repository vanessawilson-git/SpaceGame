using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts
{
    public class FinishLine : MonoBehaviour
    {
        public GameObject BaseRocket;
        private Vector2 lastPositionRocket;
        private Vector3 Movement;
        private RocketController rocket;
        private Rigidbody2D rb;
        private GameObject scrollingBackground;
       



        // Start is called before the first frame update
        void Start()
        {

            rocket = BaseRocket.GetComponent<RocketController>();
            lastPositionRocket = rocket.rb.position;

            rb = GetComponent<Rigidbody2D>();

            scrollingBackground = GameObject.Find("ScrollingBackground");
            ScrollInfinate scroll = scrollingBackground.GetComponent<ScrollInfinate>();

            Movement = new Vector3(-5f, 0, 0);

            ChangeVelocityBasedOnRocket(scroll);


            rb.velocity = Movement;

        }

        // Update is called once per frame
        void Update()
        {
       

        }


         void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Rocket"))
            {
                SceneManager.LoadScene(sceneName: "TempEndScreen");
            }

           
         }


        void ChangeVelocityBasedOnRocket(ScrollInfinate scroll) {

            if (rocket.rb.position != lastPositionRocket)
            {
                rb.velocity = new Vector2(Time.deltaTime * Input.GetAxis("Horizontal") * scroll.scrollSpeed, 0);
            }

        }

    }
}
