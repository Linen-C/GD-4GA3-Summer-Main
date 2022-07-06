using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    [Header("���݂̃G���A�i���o�[")]
    [SerializeField] int _nowArenaNo;
    [Header("�G���A���X�g(�}�j���A��)")]
    [SerializeField] GameObject[] _arenas;
    [Header("���݂̃G���A(�I�[�g)")]
    [SerializeField] GameObject _cloned;
    [Header("�ҋ@���")]
    [SerializeField] float _defWaitTime;
    [SerializeField] float _nowWaitTime;
    [SerializeField] bool _wait = false;
    [SerializeField] Animator _animator;
    [Header("�g�����X�t�H�[��(�}�j���A��)")]
    [SerializeField] Transform _entryPoint;
    [SerializeField] Transform _player;
    [Header("�R���|�[�l���g(�}�j���A��)")]
    [SerializeField] Portal _portal;
    [SerializeField] GC_GameCTRL _gameCTRL;
    [Header("�R���|�[�l���g(�I�[�g)")]
    [SerializeField] PlayerCTRL _playerCTRL;


    void Start()
    {
        _animator.SetBool("Close", false);
        _playerCTRL = _player.GetComponent<PlayerCTRL>();
        SetArena();
    }

    void Update()
    {
        if (WaitFlip() && _wait)
        {
            SetArena();
            _animator.SetBool("Close", false);
            _player.position = _entryPoint.position;
            _playerCTRL.state = PlayerCTRL.State.Alive;
            _wait = false;
        }
    }


    void SetArena()
    {
        _cloned = Instantiate(_arenas[_nowArenaNo], transform);
    }

    public void GetSignal()
    {
        Destroy(_cloned);
        _nowArenaNo++;

        if (_nowArenaNo == _arenas.Length)
        {
            _gameCTRL.S_GameClear();
            return;
        }

        _animator.SetBool("Close", true);
        _wait = true;
        _nowWaitTime = _defWaitTime;
        _playerCTRL.state = PlayerCTRL.State.Stop;
    }

    bool WaitFlip()
    {
        if (0 < _nowWaitTime)
        {
            _nowWaitTime -= Time.deltaTime;
            return false;
        }
        return true;
    }
}
