using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Enemyslider : MonoBehaviour
{
	[SerializeField]
	private Slider Hp_slider;

	private float health = 200;
	private float CurHp = 200;
	public bool isInvulnerable = false;

	public GameObject closeimg;
	public GameObject cav;

    void Start()
    {
		Hp_slider.value = (float)CurHp / (float)health;
    }

    void Update()
	{
		if (CurHp < 150)
		{
			GetComponent<Animator>().SetBool("2P_moving", false);
		}
		HandleHp();
	}
	private void HandleHp()
    {
		Hp_slider.value = Mathf.Lerp(Hp_slider.value, (float)CurHp / (float)health, Time.deltaTime * 10);
    }
    public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		CurHp -= damage;

		HandleHp();

		if (CurHp <= 150)
		{
			GetComponent<Animator>().SetBool("2P_moving",false);
		}

		if (CurHp <= 0)
		{
			Die();
		}
	}
	void Die()
    {
		Time.timeScale = 1;
		closeimg.SetActive(true);
		cav.SetActive(false);
	}
}

