# TallerDeInteraccion

Basado en el proyecto https://github.com/srcnalt/OpenAI-Unity. Es necesario crear un archivo local con la API key como se indica en el readme de dicho proyecto.

Para abrir el proyecto es necesario descargar unity primero, este proyecto se probó con Unity 2021. Luego clonar el repositorio como cualquier otro en el directorio que quieran. Al abrir unity hub en la sección "projects" se elige la opción "open" y se navega hasta la carpeta donde se clonó el repositorio. Finalmente con el proyecto abierto apretando el botón de play el programa debería correr correctamente.

La carpeta Patrimonio contiene las imagenes que el programa va a cargar. La carpeta Imagenes contiene las imagenes originales y la carpeta Mascaras las máscaras de dichas imagenes. Es necesario que las imagenes tengan los nombres apropiados. Si una imagen se llama "imagen_ejemplo.png" la máscara correspondiente a esa imagen debe llamarse "imagen_ejemplo_mask.png", es decir, agregando "_mask" al final del nombre de la imagen. 

Notar que OpenAI solo recibe imagenes cuadradas de tamaños: 256x256, 512x512 y 1024x1024.
