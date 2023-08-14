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

    // Tasa a la que aumenta/disminuye la opacidad de la imagen est�tica
    public float staticIncreaseRate, staticDecreaseRate;

    // Tasa a la que aumenta/disminuye el volumen del sonido est�tico
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

    // C�mara del salto de miedo de Slender
    public GameObject jumpscareCam;

    // Imagen negra que aparece poco despu�s de que Slender mate al jugador
    public GameObject blackscreen;

    // Destino de Slender
    Vector3 dest;

    // Entero utilizado para aleatorizar la posici�n a la que Slender se teleportar� a continuaci�n
    int randNum, randNum2;

    // Entero utilizado para aleatorizar la probabilidad de teletransportarse despu�s de que el jugador se aleja
    int teleportChance;

    // Enteros utilizados para asegurarse de que algunas cosas no ocurran m�s de una vez en el m�todo Update()
    int token, token3, token4;

    // Activa esta variable booleana si est�s usando un slider de salud
    public bool usingHealthSlider;

    // Activa esto si deseas que el cursor est� habilitado despu�s de la muerte
    public bool enableCursorAfterDeath;

    // El nombre de la escena que se cargar� 
    public string scenename;

    // Distancia de Slender al jugador
    float aiDistance;

    // Valor utilizado para la opacidad de la imagen est�tica
    float staticAmount;

    // Valor utilizado para el volumen del sonido est�tico
    float staticVolume;

    // Lista de destinos de teleportaci�n de Slender
    public List<Transform> teleportDestinations;

    // Script de raycast de Slender
    public raycastSlender raycastScript;

    // La imagen est�tica/pantalla que aparece al mirar a Slender
    public RawImage staticscreen;

    // Opacidad/color de la imagen est�tica
    public Color staticOpacity;

    // El sonido est�tico que se reproduce al mirar a Slender
    public AudioSource staticSound;

    // Sonido que se reproduce al azar cuando el jugador mira a Slender
    public AudioSource jumpscareSound;

    // C�mara del jugador
    public Camera playerCam;

    // El m�todo Start() realiza acciones al comienzo de la escena o cuando un objeto con este script se activa
    void Start()
    {
        AudioListener.pause = false; // El Audio Listener del juego no estar� en pausa
    }

    // El m�todo resetSlender() da la oportunidad a Slender de teleportarse cuando el jugador se aleja o deja de mirarlo
    void resetSlender()
    {
        teleportChance = Random.Range(0, 2); // teleportChance ser� igual a un n�mero aleatorio entre 0 y 2 (t�cnicamente, entre 0 y 1, ya que el �ltimo no es elegido)
        if (teleportChance == 0) // Si teleportChance es igual a 0, Slender se teleportar� a un destino aleatorio
        {
            randNum = Random.Range(0, teleportDestinations.Count); // randNum ser� igual a un n�mero aleatorio entre 0 y la cantidad de transforms en la lista teleportDestinations
            slenderMainTransform.position = teleportDestinations[randNum].position; // Slender se teleportar� al destino aleatorio determinado por randNum
        }
    }

    // Corrutina para cuando el jugador es asesinado
    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(3.5f); // Despu�s de 3.5 segundos,
        blackscreen.SetActive(true); // La pantalla negra se activar�
        AudioListener.pause = true; // El Audio Listener del juego se pausar� para que no haya m�s sonido
        yield return new WaitForSeconds(6f); // Despu�s de 6 segundos,
        if (enableCursorAfterDeath == true) // Si la variable enableCursorAfterDeath es verdadera,
        {
            Cursor.visible = true; // El cursor se activar� en caso de que se cargue una escena donde quieras usar el mouse (como el men� principal)
            Cursor.lockState = CursorLockMode.None; // El cursor se desbloquear� en caso de que se cargue una escena donde quieras usar el mouse (como el men� principal)
        }
        SceneManager.LoadScene(scenename); // Se cargar� la escena determinada por la cadena scenename
    }

    // El m�todo Update() realiza acciones en cada frame
    void Update()
    {
        // Obtiene los planos del frustum de la c�mara del jugador (la vista de la c�mara)
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

        // Si Slender est� dentro del campo de visi�n de la c�mara del jugador y la salud del jugador es mayor que 0,
        if (GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0)
        {
            aiDistance = Vector3.Distance(slenderMainTransform.position, player.position); // aiDistance ser� igual a la distancia entre Slender y el jugador
            Debug.Log(aiDistance); // Debug Log que muestra la distancia de Slender al jugador

            if (raycastScript.detected == true)
            {
                token3 = 0; // token3 = 0, lo que significa que la funci�n relacionada con ese token puede ocurrir nuevamente
                if (token4 == 0) // Si token4 es igual a 0,
                {
                    randNum2 = Random.Range(0, 2); // randNum2 ser� igual a un n�mero aleatorio entre 0 y 2
                    if (randNum2 == 0) // Si randNum2 es igual a 0,
                    {
                        jumpscareSound.Play(); // El sonido del salto de miedo se reproducir�
                    }
                    token4 = 1; // token4 = 1, lo que significa que esta funci�n no ocurrir� nuevamente hasta que token4 = 0
                }
                slenderMainTransform.position = slenderMainTransform.position; // La posici�n de Slender ser� igual a su propia posici�n
                staticVolume = staticVolume + soundIncreaseRate * Time.deltaTime;  // staticVolume aumentar� por la cantidad determinada por soundIncreaseRate
                staticAmount = staticAmount + staticIncreaseRate * Time.deltaTime; // staticAmount aumentar� por la cantidad determinada por staticIncreaseRate
                playerHealth = playerHealth - healthDecreaseRate * Time.deltaTime; // La salud del jugador disminuir� por la cantidad determinada por healthDecreaseRate
                if (staticVolume > 1) // Si staticVolume es mayor que 1,
                {
                    staticVolume = 1; // staticVolume ser� igual a 1
                }
                if (staticAmount > 0.9f) // Si staticAmount es mayor que 0.9,
                {
                    staticAmount = 0.9f; // staticAmount ser� igual a 0.9
                }
            }
        }
        // Si Slender est� fuera del campo de visi�n de la c�mara del jugador y la salud del jugador es mayor que 0 O si el raycast no est� golpeando al jugador y la salud del jugador es mayor que 0,
        if (!GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0 || raycastScript.detected == false && playerHealth > 0)
        {
            aiDistance = Vector3.Distance(slenderMainTransform.position, player.position); // aiDistance ser� igual a la distancia entre Slender y el jugador
            Debug.Log(aiDistance); // Debug Log que muestra la distancia de Slender al jugador

            if (token3 == 0) // Si token3 es igual a 0,
            {
                resetSlender(); // Se realizar� el m�todo resetSlender()
                token3 = 1; // token3 = 1, lo que significa que esta funci�n no ocurrir� nuevamente hasta que token3 = 0
            }
            dest = player.position; // dest ser� igual a la posici�n del jugador
            token4 = 0; // token4 = 0, lo que significa que la funci�n relacionada con ese token puede ocurrir nuevamente

            Vector3 moveDir = (dest - slenderMainTransform.position).normalized; // Direcci�n en la que se mover� Slender
            slenderMainTransform.position += moveDir * m_speed * Time.deltaTime; // Mover Slender hacia el jugador

            staticAmount = staticAmount - staticDecreaseRate * Time.deltaTime; // staticAmount disminuir� por la cantidad determinada por staticDecreaseRate
            staticVolume = staticVolume - soundDecreaseRate * Time.deltaTime; // staticVolume disminuir� por la cantidad determinada por soundDecreaseRate
            playerHealth = playerHealth + healthIncreaseRate * Time.deltaTime; // La salud del jugador aumentar� por la cantidad determinada por healthIncreaseRate
            if (staticVolume < 0) // Si staticVolume es menor que 0,
            {
                staticVolume = 0; // staticVolume ser� igual a 0
            }
            if (staticAmount < 0) // Si staticAmount es menor que 0,
            {
                staticAmount = 0; // staticAmount ser� igual a 0
            }
            if (playerHealth > 100) // Si la salud del jugador es mayor que 100,
            {
                playerHealth = 100; // La salud del jugador ser� igual a 100
            }
        }

        if (usingHealthSlider == true) // Si usingHealthSlider es verdadero,
        {
            healthSlider.value = playerHealth; // El slider de salud ser� igual al valor de la salud del jugador
        }
        staticSound.volume = staticVolume; // El volumen del Audio Source del sonido est�tico ser� igual al valor de staticVolume
        staticOpacity.a = staticAmount; // La opacidad de la variable de color staticOpacity ser� igual al valor de staticAmount
        staticscreen.color = staticOpacity; // El color de la imagen est�tica ser� igual a staticOpacity

        this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z)); // Slender siempre mirar� en direcci�n del jugador

        if (playerHealth <= 0) // Si la salud del jugador es menor o igual a 0,
        {
            StartCoroutine(killPlayer()); // Se iniciar� la corrutina killPlayer()
            staticVolume = staticVolume + soundIncreaseRate * Time.deltaTime; // staticVolume aumentar� por la cantidad determinada por soundIncreaseRate
            staticAmount = staticAmount + staticIncreaseRate * Time.deltaTime; // staticAmount aumentar� por la cantidad determinada por staticIncreaseRate
            if (staticVolume > 1) // Si staticVolume es mayor que 1,
            {
                staticVolume = 1; // staticVolume ser� igual a 1
            }
            if (staticAmount > 0.9f) // Si staticAmount es mayor que 0.9,
            {
                staticAmount = 0.9f; // staticAmount ser� igual a 0.9
            }
            player.gameObject.SetActive(false); // El objeto del jugador se desactivar�
            jumpscareCam.SetActive(true); // Se activar� la c�mara del salto de miedo de Slender
            m_speed = 0; // La velocidad de Slender ser� igual a 0
        }
        if (aiDistance <= catchDistance) // Si la distancia de Slender al jugador es menor o igual a catchDistance,
        {
            if (token == 0) // Si token es igual a 0,
            {
                playerHealth = 0; // La salud del jugador ser� igual a 0
                token = 1; // token = 1, lo que significa que esta funci�n no ocurrir� nuevamente hasta que token = 0
                // El jugador es asesinado 
            }
        }
    }
}
