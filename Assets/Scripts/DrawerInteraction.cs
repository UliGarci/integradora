using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float interactionDistance = 2f; // Distancia m�nima para interactuar
    public float moveSpeed = 2f; // Velocidad del movimiento del caj�n
    private Vector3 closedPosition; // Posici�n cerrada del caj�n
    private Vector3 openPosition; // Posici�n abierta del caj�n
    private bool isOpen = false; // Estado del caj�n
    private bool playerNearby = false; // Si el jugador est� cerca

    void Start()
    {
        // Definir las posiciones cerrada y abierta seg�n lo especificado
        closedPosition = new Vector3(-0.195f, 0.1260897f, 0.009f);
        openPosition = new Vector3(0.159f, 0.1260897f, 0.009f);

        // Asegurarse de que el caj�n comience en la posici�n cerrada
        transform.position = closedPosition;
    }

    void Update()
    {
        // Comprobar si el jugador est� cerca y mirando el caj�n
        playerNearby = Vector3.Distance(player.position, transform.position) <= interactionDistance;

        if (playerNearby && IsPlayerLookingAtDrawer())
        {
            Debug.Log("Presiona 'E' para interactuar con el caj�n.");

            // Detectar interacci�n
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleDrawer();
            }
        }

        // Mover el caj�n hacia la posici�n abierta o cerrada
        MoveDrawer();
    }

    private bool IsPlayerLookingAtDrawer()
    {
        // Obtener la direcci�n a la que el jugador est� mirando
        Vector3 directionToDrawer = (transform.position - player.position).normalized;
        float dotProduct = Vector3.Dot(player.forward, directionToDrawer);

        // Considerar que el jugador est� mirando si el �ngulo es menor a 45 grados
        return dotProduct > 0.7f; // Ajusta este valor seg�n sea necesario
    }

    private void ToggleDrawer()
    {
        isOpen = !isOpen; // Cambiar el estado del caj�n
        Debug.Log(isOpen ? "Caj�n abri�ndose..." : "Caj�n cerr�ndose...");
    }

    private void MoveDrawer()
    {
        // Interpolar la posici�n del caj�n hacia abierta o cerrada
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Salida de consola para monitorear el movimiento
        Debug.Log($"Moviendo caj�n hacia: {targetPosition}. Posici�n actual: {transform.position}");
    }
}
