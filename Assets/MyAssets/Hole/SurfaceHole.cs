using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHole : MonoBehaviour
{
    // Funcionalidad Hoyo Original:
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.GetComponent<HoleTrigger>())
        {
            // Aqui obtenemos la posicion de los slots del jugador,
            // Esto es para comunicarselo al evento que le enviara la informacion a la camara
            // Se llama al evento con la posicion del hoyo y el slot del jugador
            MyGameManager.Instance.HoleFallEventTrigger(transform.position);
        }
    }
    /*
    [SerializeField] float horizontalRadius = 5f; // Radio horizontal (x)
    [SerializeField] float verticalRadius = 3f;   // Radio vertical (y)
    [SerializeField] float attractionForce = 5f;  // Fuerza de atracci�n

    private void FixedUpdate()
    {
        // Obtener todos los colisionadores cercanos (usa un rango amplio para incluir candidatos)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Max(horizontalRadius, verticalRadius));

        foreach (var collider in colliders)
        {
            // Aseg�rate de no atraer al mismo atractor
            if (collider.gameObject == gameObject) continue;

            // Verifica si el objeto est� dentro del �rea el�ptica
            if (IsInsideEllipse(collider.transform.position))
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Calcular direcci�n hacia el centro del atractor
                    Vector2 direction = (transform.position - collider.transform.position).normalized;

                    // Aplicar fuerza de atracci�n
                    rb.AddForce(direction * attractionForce);
                }
            }
        }
    }

    // M�todo para comprobar si una posici�n est� dentro de la elipse
    private bool IsInsideEllipse(Vector2 position)
    {
        // Transformar la posici�n al espacio relativo del atractor
        Vector2 relativePosition = position - (Vector2)transform.position;

        // Ecuaci�n de la elipse: (x^2 / a^2) + (y^2 / b^2) <= 1
        float xNormalized = relativePosition.x / horizontalRadius;
        float yNormalized = relativePosition.y / verticalRadius;

        return (xNormalized * xNormalized + yNormalized * yNormalized) <= 1f;
    }

    // Visualizaci�n del �rea el�ptica en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Dibujar el contorno de la elipse
        for (int i = 0; i <= 360; i++)
        {
            float angle = Mathf.Deg2Rad * i;
            float x = horizontalRadius * Mathf.Cos(angle);
            float y = verticalRadius * Mathf.Sin(angle);

            // Transformar los puntos locales a la posici�n del atractor
            Vector3 point = transform.position + new Vector3(x, y, 0f);
            if (i > 0)
                Gizmos.DrawLine(prevPoint, point);

            prevPoint = point;
        }
    }

    private Vector3 prevPoint;
    */
    public float maxAttractionForce = 10f; // Fuerza m�xima de atracci�n
    public float minAttractionForce = 1f; // Fuerza m�nima de atracci�n
    public float horizontalRadius = 5f;   // Semieje horizontal del �rea ovalada
    public float verticalRadius = 3f;     // Semieje vertical del �rea ovalada

    private void FixedUpdate()
    {
        // Detectar si el jugador est� cerca
        GameObject player = FindPlayerInRange();

        if (player != null)
        {
            // Obtener el Rigidbody2D del jugador
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calcular la fuerza de atracci�n en funci�n de la distancia
                float distance = Vector2.Distance(transform.position, player.transform.position);
                float attractionForce = CalculateAttractionForce(distance);

                // Calcular direcci�n hacia el centro del atractor
                Vector2 direction = (transform.position - player.transform.position).normalized;

                // Aplicar fuerza de atracci�n al jugador
                rb.AddForce(direction * attractionForce);
            }
        }
    }

    // M�todo para detectar si el jugador est� dentro del �rea ovalada
    private GameObject FindPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Max(horizontalRadius, verticalRadius));

        foreach (var collider in colliders)
        {
            PlayerMotor playerManager = collider.GetComponent<PlayerMotor>();
            if (playerManager != null && IsInsideEllipse(collider.transform.position))
            {
                return collider.gameObject;
            }
        }

        return null;
    }

    // Verificar si una posici�n est� dentro del �rea ovalada
    private bool IsInsideEllipse(Vector2 position)
    {
        Vector2 relativePosition = position - (Vector2)transform.position;

        // Ecuaci�n de la elipse: (x^2 / a^2) + (y^2 / b^2) <= 1
        float xNormalized = relativePosition.x / horizontalRadius;
        float yNormalized = relativePosition.y / verticalRadius;

        return (xNormalized * xNormalized + yNormalized * yNormalized) <= 1f;
    }

    // Calcular la fuerza de atracci�n seg�n la distancia
    private float CalculateAttractionForce(float distance)
    {
        // Mapea la distancia a un rango entre la fuerza m�nima y m�xima
        float maxDistance = Mathf.Max(horizontalRadius, verticalRadius);
        float normalizedDistance = Mathf.Clamp01(1 - (distance / maxDistance));

        return Mathf.Lerp(minAttractionForce, maxAttractionForce, normalizedDistance);
    }

    // Visualizar el �rea ovalada en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i <= 360; i++)
        {
            float angle = Mathf.Deg2Rad * i;
            float x = horizontalRadius * Mathf.Cos(angle);
            float y = verticalRadius * Mathf.Sin(angle);

            Vector3 point = transform.position + new Vector3(x, y, 0f);
            if (i > 0)
                Gizmos.DrawLine(prevPoint, point);

            prevPoint = point;
        }
    }

    private Vector3 prevPoint;
}
