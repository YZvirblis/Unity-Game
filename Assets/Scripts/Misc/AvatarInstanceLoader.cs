using UnityEngine;

namespace ReadyPlayerMe
{
    public class AvatarInstanceLoader : MonoBehaviour
    {
        [SerializeField]
        private string avatarURL = "https://d1a370nemizbjq.cloudfront.net/209a1bc2-efed-46c5-9dfd-edc8a1d9cbe4.glb";
        [SerializeField]
        GameObject avatar;

        private void Start()
        {
            var player = gameObject;
            Debug.Log($"Started loading avatar");
            AvatarLoader avatarLoader = new AvatarLoader();
            avatarLoader.OnCompleted += (sender, args) =>
            {
                OnAvatarLoaded(args.Avatar);
                avatarLoader.OnCompleted += AvatarLoadComplete;
            };
            avatarLoader.OnFailed += AvatarLoadFail;
            avatarLoader.LoadAvatar(avatarURL);
        }

        private void AvatarLoadComplete(object sender, CompletionEventArgs args)
        {
            Debug.Log($"Avatar loaded");
        }

        private void AvatarLoadFail(object sender, FailureEventArgs args)
        {
            Debug.Log($"Avatar loading failed with error message: {args.Message}");
        }
        private void OnAvatarLoaded(GameObject loadedAvatar)
        {
            avatar = loadedAvatar;
            avatar.transform.parent = gameObject.transform;
            avatar.transform.position = gameObject.transform.position;
            avatar.transform.rotation = gameObject.transform.rotation;
            avatar.name = "Player";
        }
    }
}