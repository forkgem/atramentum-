using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// InteractTrigger — Unity 6.2
/// 
/// HOW TO USE:
///   1. Add this script to a GameObject that has a Box Collider (set "Is Trigger" = true).
///   2. In the Inspector, wire up the "On Triggered" UnityEvent to whatever you want to happen.
///   3. Make sure your Player GameObject has the tag "Player".
///   4. Walk into the box and press E — the event fires.
/// 
/// OPTIONAL:
///   - Set "Interact Key" in the Inspector (defaults to E).
///   - Toggle "Show Prompt" to display a world-space UI hint (requires a Canvas child named "PromptCanvas").
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class InteractTrigger : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("Key the player must press to activate the trigger.")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Tooltip("Tag used to identify the player object.")]
    [SerializeField] private string playerTag = "Player";

    [Header("Events")]
    [Tooltip("Fires when the player presses the interact key inside the trigger zone.")]
    public UnityEvent onTriggered;

    [Tooltip("Fires when the player enters the trigger zone.")]
    public UnityEvent onPlayerEnter;

    [Tooltip("Fires when the player exits the trigger zone.")]
    public UnityEvent onPlayerExit;

    [Header("Options")]
    [Tooltip("If true, the trigger can only be activated once.")]
    [SerializeField] private bool activateOnce = false;

    [Tooltip("Show an optional prompt hint object when the player is inside the zone.")]
    [SerializeField] private GameObject promptObject;

    // ── State ──────────────────────────────────────────────────────────────────
    private bool _playerInside  = false;
    private bool _hasActivated  = false;

    // ── Unity Messages ─────────────────────────────────────────────────────────
    private void Awake()
    {
        // Ensure the collider is flagged as a trigger at runtime
        var col = GetComponent<BoxCollider>();
        col.isTrigger = true;

        SetPromptVisible(false);
    }

    private void Update()
    {
        if (!_playerInside) return;
        if (activateOnce && _hasActivated) return;

        if (Input.GetKeyDown(interactKey))
        {
            Activate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        _playerInside = true;
        SetPromptVisible(true);
        onPlayerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        _playerInside = false;
        SetPromptVisible(false);
        onPlayerExit?.Invoke();
    }

    // ── Public API ─────────────────────────────────────────────────────────────

    /// <summary>Manually activate the trigger from code.</summary>
    public void Activate()
    {
        if (activateOnce && _hasActivated) return;

        _hasActivated = true;
        onTriggered?.Invoke();

        if (activateOnce)
            SetPromptVisible(false);
    }

    /// <summary>Reset the trigger so it can be activated again (useful when activateOnce = true).</summary>
    public void ResetTrigger()
    {
        _hasActivated = false;
        if (_playerInside)
            SetPromptVisible(true);
    }

    // ── Helpers ────────────────────────────────────────────────────────────────
    private void SetPromptVisible(bool visible)
    {
        if (promptObject != null)
            promptObject.SetActive(visible);
    }

    // ── Editor Gizmo ───────────────────────────────────────────────────────────
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var col = GetComponent<BoxCollider>();
        if (col == null) return;

        Gizmos.color = _playerInside
            ? new Color(0f, 1f, 0f, 0.35f)
            : new Color(0f, 0.8f, 1f, 0.2f);

        var worldCenter = transform.TransformPoint(col.center);
        var worldSize   = Vector3.Scale(col.size, transform.lossyScale);

        Gizmos.matrix = Matrix4x4.TRS(worldCenter, transform.rotation, Vector3.one);
        Gizmos.DrawCube(Vector3.zero, worldSize);

        Gizmos.color = _playerInside
            ? new Color(0f, 1f, 0f, 0.9f)
            : new Color(0f, 0.8f, 1f, 0.7f);
        Gizmos.DrawWireCube(Vector3.zero, worldSize);
    }
#endif
}
