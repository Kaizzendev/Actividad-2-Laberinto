using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] internal float speed = 3;
        [SerializeField] internal float rotSpeed = 3;
        [SerializeField] internal Camera playerCamera;


        public bool isPlaying;
        public float vida;
        public float puntos;

        public bool enemigo_a_tiro;
        public bool enemigo_muerto;

        private Animator animator;

        private CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            
            vida = 100f;
            puntos = 0f;
            enemigo_a_tiro = false;
            enemigo_muerto = false;

            animator = transform.GetChild(0).GetComponent<Animator>();
            animator.enabled = true;
            
        }

        void Update()
        {
            animator.SetBool("enemigo_a_tiro", enemigo_a_tiro);
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            if (isPlaying == false) return; // Esta linea no deja mover al jugador hasta darle al play


            actualiza_vida_puntos();

            float yInput = Input.GetAxis("Vertical");
            float xInput = Input.GetAxis("Horizontal");

            Vector3 localMove = new Vector3(0, 0, yInput);
            Vector3 move = transform.TransformDirection(localMove) * speed * Time.deltaTime;

            float movimiento = yInput*100f;
            
            animator.SetFloat("movimiento",movimiento);

            Debug.Log(movimiento);
            

            transform.Rotate(Vector3.up * xInput * rotSpeed * Time.deltaTime);

            controller.Move(move);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);


                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Button"))
                    {
                        hit.collider.GetComponentInParent<Door>().Activate();
                        puntos += 25f;  // cuando abre una puerta gana 25 puntos
                    }
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (enemigo_a_tiro)
                {
                    animator.SetBool("enemigo_a_tiro", enemigo_a_tiro);
                    enemigo_a_tiro = false;
                    enemigo_muerto = true;
                }
                puntos += 25f;
                if (puntos > 100f) puntos = 100f;
            }

        }

        public void actualiza_vida_puntos()
        {
            GameManager.Instance.Vida_puntos(vida, puntos);
        }

        private void OnTriggerEnter(Collider other) // Al tocar el trofeo ganas!
        {
            if (other.gameObject.CompareTag("Win"))
            {
                GameManager.Instance.Win();
            }

            // si toca con una trampa pierde 25 de vida y 5 puntos, si la vida se queda a cero pero tiene
            // mas de 20 puntos coge 20 puuntos y le suma otros 100 de vida

            if (other.gameObject.CompareTag("Trap"))
            {
                if (vida >= 0f) vida -= 25f;
                if (puntos >= 0f) puntos -= 5f;

                if (vida <= 0f && puntos >= 20f)
                {
                    puntos -= 20f;
                    vida = 100f;
                }

                if (puntos <= 0f) puntos = 0f;

                if (vida <= 0f) GameManager.Instance.Die();
            }
        }
    }
}