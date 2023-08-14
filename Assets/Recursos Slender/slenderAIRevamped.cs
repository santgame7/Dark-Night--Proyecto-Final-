using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class slenderAIRevamped : MonoBehaviour
{
    // Componente Mesh Renderer de Slender
    public SkinnedMeshRenderer slenderMesh;

    // Velocidad de movimiento de Slender
    public float m_speed;

    // Tasa a la que aumenta/disminuye la opacidad de la imagen estática
    public float staticIncreaseRate, staticDecreaseRate;

    // Tasa a la que aumenta/disminuye el volumen del sonido estático
    public float soundIncreaseRate, soundDecreaseRate;

    // Tasa a la que aumenta/disminuye la salud del jugador
    public float healthIncreaseRate, healthDecreaseRate;

    // Distancia desde la que Slender atrapa al jugador si se acerca demasiado
    public float catchDistance;

    // Salud del jugador
    public float playerHealth = 100;

    public Slider healthSlider;

    // Transform del jugador
    public Transform player;

    // Transform principal de Slender
    public Transform slenderMainTransform;

    // Cámara del salto de miedo de Slender
    public GameObject jumpscareCam;

    // Imagen negra que aparece poco después de que Slender mate al jugador
    public GameObject blackscreen;

    // Destino de Slender
    Vector3 dest;

    // Entero utilizado para aleatorizar la posición a la que Slender se teleportará a continuación
    int randNum, randNum2;

    // Entero utilizado para aleatorizar la probabilidad de teletransportarse después de que el jugador se aleja
    int teleportChance;

    // Enteros utilizados para asegurarse de que algunas cosas no ocurran más de una vez en el método Update()
    int token, token3, token4;

    // Activa esta variable booleana si estás usando un slider de salud
    public bool usingHealthSlider;

    // Activa esto si deseas que el cursor esté habilitado después de la muerte
    public bool enableCursorAfterDeath;

    // El nombre de la escena que se cargará 
    public string scenename;

    // Distancia de Slender al jugador
    float aiDistance;

    // Valor utilizado para la opacidad de la imagen estática
    float staticAmount;

    // Valor utilizado para el volumen del sonido estático
    float staticVolume;

    // Lista de destinos de teleportación de Slender
    public List<Transform> teleportDestinations;

    // Script de raycast de Slender
    public raycastSlender raycastScript;

    // La imagen estática/pantalla que aparece al mirar a Slender
    public RawImage staticscreen;

    // Opacidad/color de la imagen estática
    public Color staticOpacity;

    // El sonido estático que se reproduce al mirar a Slender
    public AudioSource staticSound;

    // Sonido que se reproduce al azar cuando el jugador mira a Slender
    public AudioSource jumpscareSound;

    // Cámara del jugador
    public Camera playerCam;

    // El método Start() realiza acciones al comienzo de la escena o cuando un objeto con este script se activa
    void Start()
    {
        AudioListener.pause = false; // El Audio Listener del juego no estará en pausa
    }

    // El método resetSlender() da la oportunidad a Slender de teleportarse cuando el jugador se aleja o deja de mirarlo
    void resetSlender()
    {
        teleportChance = Random.Range(0, 2); // teleportChance será igual a un número aleatorio entre 0 y 2 (técnicamente, entre 0 y 1, ya que el último no es elegido)
        if (teleportChance == 0) // Si teleportChance es igual a 0, Slender se teleportará a un destino aleatorio
        {
            randNum = Random.Range(0, teleportDestinations.Count); // randNum será igual a un número aleatorio entre 0 y la cantidad de transforms en la lista teleportDestinations
            slenderMainTransform.position = teleportDestinations[randNum].position; // Slender se teleportará al destino aleatorio determinado por randNum
        }
    }

    // Corrutina para cuando el jugador es asesinado
    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(3.5f); // Después de 3.5 segundos,
        blackscreen.SetActive(true); // La pantalla negra se activará
        AudioListener.pause = true; // El Audio Listener del juego se pausará para que no haya más sonido
        yield return new WaitForSeconds(6f); // Después de 6 segundos,
        if (enableCursorAfterDeath == true) // Si la variable enableCursorAfterDeath es verdadera,
        {
            Cursor.visible = true; // El cursor se activará en caso de que se cargue una escena donde quieras usar el mouse (como el menú principal)
            Cursor.lockState = CursorLockMode.None; // El cursor se desbloqueará en caso de que se cargue una escena donde quieras usar el mouse (como el menú principal)
        }
        SceneManager.LoadScene(scenename); // Se cargará la escena determinada por la cadena scenename
    }

    // El método Update() realiza acciones en cada frame
    void Update()
    {
        // Obtiene los planos del frustum de la cámara del jugador (la vista de la cámara)
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

        // Si Slender está dentro del campo de visión de la cámara del jugador y la salud del jugador es mayor que 0,
        if (GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0)
        {
            aiDistance = Vector3.Distance(slenderMainTransform.position, player.position); // aiDistance será igual a la distancia entre Slender y el jugador
            Debug.Log(aiDistance); // Debug Log que muestra la distancia de Slender al jugador

            if (raycastScript.detected == true)
            {
                token3 = 0; // token3 = 0, lo que significa que la función relacionada con ese token puede ocurrir nuevamente
                if (token4 == 0) // Si token4 es igual a 0,
                {
                    randNum2 = Random.Range(0, 2); // randNum2 será igual a un número aleatorio entre 0 y 2
                    if (randNum2 == 0) // Si randNum2 es igual a 0,
                    {
                        jumpscareSound.Play(); // El sonido del salto de miedo se reproducirá
                    }
                    token4 = 1; // token4 = 1, lo que significa que esta función no ocurrirá nuevamente hasta que token4 = 0
                }
                slenderMainTransform.position = slenderMainTransform.position; // La posición de Slender será igual a su propia posición
                staticVolume = staticVolume + soundIncreaseRate * Time.deltaTime;  // staticVolume aumentará por la cantidad determinada por soundIncreaseRate
                staticAmount = staticAmount + staticIncreaseRate * Time.deltaTime; // staticAmount aumentará por la cantidad determinada por staticIncreaseRate
                playerHealth = playerHealth - healthDecreaseRate * Time.deltaTime; // La salud del jugador disminuirá por la cantidad determinada por healthDecreaseRate
                if (staticVolume > 1) // Si staticVolume es mayor que 1,
                {
                    staticVolume = 1; // staticVolume será igual a 1
                }
                if (staticAmount > 0.9f) // Si staticAmount es mayor que 0.9,
                {
                    staticAmount = 0.9f; // staticAmount será igual a 0.9
                }
            }
        }
        // Si Slender está fuera del campo de visión de la cámara del jugador y la salud del jugador es mayor que 0 O si el raycast no está golpeando al jugador y la salud del jugador es mayor que 0,
        if (!GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0 || raycastScript.detected == false && playerHealth > 0)
        {
            aiDistance = Vector3.Distance(slenderMainTransform.position, player.position); // aiDistance será igual a la distancia entre Slender y el jugador
            Debug.Log(aiDistance); // Debug Log que muestra la distancia de Slender al jugador

            if (token3 == 0) // Si token3 es igual a 0,
            {
                resetSlender(); // Se realizará el método resetSlender()
                token3 = 1; // token3 = 1, lo que significa que esta función no ocurrirá nuevamente hasta que token3 = 0
            }
            dest = player.position; // dest será igual a la posición del jugador
            token4 = 0; // token4 = 0, lo que significa que la función relacionada con ese token puede ocurrir nuevamente

            Vector3 moveDir = (dest - slenderMainTransform.position).normalized; // Dirección en la que se moverá Slender
            slenderMainTransform.position += moveDir * m_speed * Time.deltaTime; // Mover Slender hacia el jugador

            staticAmount = staticAmount - staticDecreaseRate * Time.deltaTime; // staticAmount disminuirá por la cantidad determinada por staticDecreaseRate
            staticVolume = staticVolume - soundDecreaseRate * Time.deltaTime; // staticVolume disminuirá por la cantidad determinada por soundDecreaseRate
            playerHealth = playerHealth + healthIncreaseRate * Time.deltaTime; // La salud del jugador aumentará por la cantidad determinada por healthIncreaseRate
            if (staticVolume < 0) // Si staticVolume es menor que 0,
            {
                staticVolume = 0; // staticVolume será igual a 0
            }
            if (staticAmount < 0) // Si staticAmount es menor que 0,
            {
                staticAmount = 0; // staticAmount será igual a 0
            }
            if (playerHealth > 100) // Si la salud del jugador es mayor que 100,
            {
                playerHealth = 100; // La salud del jugador será igual a 100
            }
        }

        if (usingHealthSlider == true) // Si usingHealthSlider es verdadero,
        {
            healthSlider.value = playerHealth; // El slider de salud será igual al valor de la salud del jugador
        }
        staticSound.volume = staticVolume; // El volumen del Audio Source del sonido estático será igual al valor de staticVolume
        staticOpacity.a = staticAmount; // La opacidad de la variable de color staticOpacity será igual al valor de staticAmount
        staticscreen.color = staticOpacity; // El color de la imagen estática será igual a staticOpacity

        this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z)); // Slender siempre mirará en dirección del jugador

        if (playerHealth <= 0) // Si la salud del jugador es menor o igual a 0,
        {
            StartCoroutine(killPlayer()); // Se iniciará la corrutina killPlayer()
            staticVolume = staticVolume + soundIncreaseRate * Time.deltaTime; // staticVolume aumentará por la cantidad determinada por soundIncreaseRate
            staticAmount = staticAmount + staticIncreaseRate * Time.deltaTime; // staticAmount aumentará por la cantidad determinada por staticIncreaseRate
            if (staticVolume > 1) // Si staticVolume es mayor que 1,
            {
                staticVolume = 1; // staticVolume será igual a 1
            }
            if (staticAmount > 0.9f) // Si staticAmount es mayor que 0.9,
            {
                staticAmount = 0.9f; // staticAmount será igual a 0.9
            }
            player.gameObject.SetActive(false); // El objeto del jugador se desactivará
            jumpscareCam.SetActive(true); // Se activará la cámara del salto de miedo de Slender
            m_speed = 0; // La velocidad de Slender será igual a 0
        }
        if (aiDistance <= catchDistance) // Si la distancia de Slender al jugador es menor o igual a catchDistance,
        {
            if (token == 0) // Si token es igual a 0,
            {
                playerHealth = 0; // La salud del jugador será igual a 0
                token = 1; // token = 1, lo que significa que esta función no ocurrirá nuevamente hasta que token = 0
                // El jugador es asesinado 
            }
        }
    }
}
