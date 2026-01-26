# 🧩 Actividad – Laberinto

## 📌 Descripción general

Esta es la **actividad** para la asignatura **Motores de Videojuegos I**.

El proyecto consiste en **escapar de un laberinto** y **tocar un trofeo** para completar el nivel.

El jugador puede **cambiar de cámara** durante la partida.

---

## 🔄 Actualización – Segunda entrega

Para la segunda entrega se han añadido nuevas mecánicas y trampas que enriquecen la jugabilidad:

### 🚪 Puertas con código de color

* Las puertas se abren al **interactuar con la tecla `E`**.
* Es necesario que el **raycast apunte al botón** correspondiente.
* Al abrirse, se activa:

  * Una **animación**
  * Un **cambio de cámara**

### ⚠️ Nuevas trampas

Se han añadido **tres tipos de trampas**:

* **Trampa de bola** 🟠
  Una bola que se mueve de un lado a otro por los pasillos.

* **Trampa de pinchos** 🔺
  Pinchos que salen del suelo o de la pared.

* **Trampa de pinchos activable** 💥
  Al pisar una losa, los pinchos salen disparados hacia el jugador.

---

## 🎮 Estado del código

* Los **scripts funcionan correctamente**, pero se han diseñado de la forma **más simple posible**.
* El **Game Manager**:

  * Debería tener más seguridad para comportarse correctamente como **Singleton**.
  * Sería recomendable implementar una **máquina de estados** para gestionar:

    * Jugando
    * Pausado
    * Victoria

---

## 🎥 Enlaces

### 📹 Vídeos

* **Vídeo explicativo (primera entrega):**
  [https://1drv.ms/v/c/40d7f72d38f468c7/IQD0ACC-l9-xTIFoyb3_YRIEAbrC7BjK7AGLAQjjtkm1pvA?e=Km6Paw](https://1drv.ms/v/c/40d7f72d38f468c7/IQD0ACC-l9-xTIFoyb3_YRIEAbrC7BjK7AGLAQjjtkm1pvA?e=Km6Paw)

* **Vídeo segunda entrega:**
  [https://1drv.ms/v/c/40d7f72d38f468c7/IQAD0pZzAWl3SYPySzcL3QRMAWV_GeMVoAYet-FE0_ZCS6k?e=PdPQcr](https://1drv.ms/v/c/40d7f72d38f468c7/IQAD0pZzAWl3SYPySzcL3QRMAWV_GeMVoAYet-FE0_ZCS6k?e=PdPQcr)

### 🌐 Itch.io

* [https://kaizzendev.itch.io/laberinto-actividad-2](https://kaizzendev.itch.io/laberinto-actividad-2)

---

## 🛠️ Autor

**KaizzenDev**
