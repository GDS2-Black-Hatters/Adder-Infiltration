using UnityEngine;

public class CommentComponent : MonoBehaviour
{
    [SerializeField, TextArea(5, 20)] private string comment;
}
