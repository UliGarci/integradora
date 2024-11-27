using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float interactionDistance = 2f; // Distancia mínima para interactuar
    public float moveSpeed = 2f; // Velocidad del movimiento del cajón
    private Vector3 closedPosition; // Posición cerrada del cajón
    private Vector3 openPosition; // Posición abierta del cajón
    private bool isOpen = false; // Estado del cajón
    private bool playerNearby = false; // Si el jugador está cerca

    void Start()
    {
        // Definir las posiciones cerrada y abierta según lo especificado
        closedPosition = new Vector3(-0.195f, 0.1260897f, 0.009f);
        openPosition = new Vector3(0.159f, 0.1260897f, 0.009f);

        // Asegurarse de que el cajón comience en la posición cerrada
        transform.position = closedPosition;
    }

    void Update()
    {
        // Comprobar si el jugador está cerca y mirando el cajón
        playerNearby = Vector3.Distance(player.position, transform.position) <= interactionDistance;

        if (playerNearby && IsPlayerLookingAtDrawer())
        {
            Debug.Log("Presiona 'E' para interactuar con el cajón.");

            // Detectar interacción
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleDrawer();
            }
        }

        // Mover el cajón hacia la posición abierta o cerrada
        MoveDrawer();
    }

    private bool IsPlayerLookingAtDrawer()
    {
        // Obtener la dirección a la que el jugador está mirando
        Vector3 directionToDrawer = (transform.position - player.position).normalized;
        float dotProduct = Vector3.Dot(player.forward, directionToDrawer);

        // Considerar que el jugador está mirando si el ángulo es menor a 45 grados
        return dotProduct > 0.7f; // Ajusta este valor según sea necesario
    }

    private void ToggleDrawer()
    {
        isOpen = !isOpen; // Cambiar el estado del cajón
        Debug.Log(isOpen ? "Cajón abriéndose..." : "Cajón cerrándose...");
    }

    private void MoveDrawer()
    {
        // Interpolar la posición del cajón hacia abierta o cerrada
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Salida de consola para monitorear el movimiento
        Debug.Log($"Moviendo cajón hacia: {targetPosition}. Posición actual: {transform.position}");
    }
}
