using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    const string VERTEX_OFFSET = "_vertex_offset";

    public ZoneType type = ZoneType.NEUTRAL;
    [SerializeField] private Vector3 rotation = Vector3.zero;
    [Tooltip("Angle per second")]
    [SerializeField] private float rotationRate = 0f;
    [SerializeField] float defaultVertexOffset = 0.2f;
    [SerializeField] float hitVertexOffset = 0.6f;
    [SerializeField] float vertexTransitionSpeed = 1f;



    Renderer thisRenderer = null;
    


    private void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        print(thisRenderer.material.shader);
    }


    private void Update()
    {
        transform.Rotate(rotation,rotationRate * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.transform.GetComponent<ICollectable>();
        if (collectable!=null && !other.transform.CompareTag("striker"))
        {
            ZoneType collectableType = collectable.GetCollectableType();
            collectable.Collect(type);
            OnZoneHit(collectableType);


        }
    }

    private void OnTriggerStay(Collider other)
    {
        ICollectable collectable = other.transform.GetComponent<ICollectable>();
        if (collectable != null && !other.transform.CompareTag("striker"))
        {
            ZoneType collectableType = collectable.GetCollectableType();
            collectable.Collect(type);
            OnZoneHit(collectableType);
            

        }
    }

    private void SetVertexOffset(float offset)
    {
        thisRenderer.material.SetFloat(VERTEX_OFFSET, offset);
    }

    private void OnZoneHit(ZoneType collectableType)
    {
        StopCoroutine(SmoothlyResetVertexOffset());

        if (collectableType == type)
        {
            SetVertexOffset(hitVertexOffset);
        }
        else
        {
            SetVertexOffset(-hitVertexOffset);
        }
        
        
        StartCoroutine(SmoothlyResetVertexOffset());
    }

    private float GetVertexOffset()
    {
        float currentVertexOffset = thisRenderer.material.GetFloat(VERTEX_OFFSET);
        return currentVertexOffset;
    }

    IEnumerator SmoothlyResetVertexOffset()
    {
        while (true)
        {
            float currentVertexOffset = GetVertexOffset();
            float newVertexOffset = Mathf.Lerp(currentVertexOffset,defaultVertexOffset,Time.deltaTime*vertexTransitionSpeed);
            newVertexOffset = Mathf.Clamp(newVertexOffset,-hitVertexOffset,hitVertexOffset);
            SetVertexOffset(newVertexOffset);
            yield return null;
        }
    }

  
}
