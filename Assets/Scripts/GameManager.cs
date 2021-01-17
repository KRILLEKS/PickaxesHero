using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public void LoadCity() =>
   SceneManager.LoadScene("City");

  public void LoadMine() =>
  SceneManager.LoadScene("Mine");
}
