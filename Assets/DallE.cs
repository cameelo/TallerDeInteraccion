using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System;
using TMPro;

namespace OpenAI
{
    public class DallE : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private Button proceedButton;
        [SerializeField] private Image image;
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private TMP_Text headerText;
        private ImageCollection imageCollection;
        [SerializeField] private GameObject loadingLabel;
        [SerializeField] private bool useDropdown;
        private int collectionIndex = 0;

        private OpenAIApi openai = new OpenAIApi();

        private IEnumerator Start()
        {
            imageCollection = GameObject.FindObjectOfType<ImageCollection>();
            yield return new WaitUntil(() => imageCollection.getIsInitialized());
            button.onClick.AddListener(SendImageRequest);
            headerText.text = "Describe a este patrimonio...";
            proceedButton.gameObject.SetActive(false);
            if (!useDropdown)
            {
                dropdown.gameObject.SetActive(false);
                int collectionSize = imageCollection.getSize();
                System.Random rnd = new System.Random();
                collectionIndex = rnd.Next(collectionSize);

                Texture2D selectedImage = new Texture2D(2, 2);
                byte[] imageBytes = File.ReadAllBytes(imageCollection.getImage(collectionIndex));
                selectedImage.LoadImage(imageBytes);
                var sprite = Sprite.Create(selectedImage, new Rect(0, 0, selectedImage.width, selectedImage.height), Vector2.zero, 1f);
                image.sprite = sprite;
            }
            
        }

        private async void SendImageRequest()
        {
            //loadingLabel.SetActive(true);
            button.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            headerText.SetText("Reconstruyendo patrimonio");
            image.gameObject.GetComponent<AnimateImage>().StartAnimation();
            image.gameObject.GetComponent<Pixelation>().StopPixelating();

            string selectedImage;
            string selectedMask;
            if (useDropdown)
            {
                selectedImage = imageCollection.getImage(dropdown.value - 1);
                selectedMask = imageCollection.getMask(dropdown.value - 1);
            }
            else
            {
                selectedImage = imageCollection.getImage(collectionIndex);
                selectedMask = imageCollection.getMask(collectionIndex);
            }
            


            var response = await openai.CreateImageEdit(new CreateImageEditRequest
            {
                Image = selectedImage,
                Mask = selectedMask,
                Prompt = inputField.text,
                Size = ImageSize.Size256
            });

            if (response.Data != null && response.Data.Count > 0)
            {
                using (var request = new UnityWebRequest(response.Data[0].Url))
                {
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                    request.SendWebRequest();

                    while (!request.isDone) await Task.Yield();

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(request.downloadHandler.data);
                    var sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero, 1f);
                    image.sprite = sprite;

                    image.gameObject.GetComponent<Pixelation>().StartDepixelating();
                    //Save generated image to file
                    imageCollection.addAndSaveImage(request.downloadHandler.data);
                }
            }
            else
            {
                Debug.LogWarning("No image was created from this prompt.");
            }

            //button.enabled = true;
            //inputField.enabled = true;
            //loadingLabel.SetActive(false);
            headerText.SetText("Patrimonio reconstruido");
            proceedButton.gameObject.SetActive(true);
        }
    }
}
