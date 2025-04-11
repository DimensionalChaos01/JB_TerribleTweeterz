using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class Monster : MonoBehaviour
{

    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;

    [SerializeField] float _damagespeed = -0.5f;
    bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shoulddiefromCollision(collision))
        {
            StartCoroutine(Die());
        }

    }

    bool shoulddiefromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < _damagespeed)
            return true;

        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);
    }
}
