using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class RouletteManager : MonoBehaviour
{
    public Transform piecePrefab;
    public Transform linePrefab;

    public Transform pieceParent;
    public Transform lineParent;

    [SerializeField] private RoulettePieceData[] roulettePieceDatas;

    private float pieceAngle; //�ǽ����� ��ġ�Ǵ� ����
    private float halfPieceAngle; // �ǽ����� ��ġ�Ǵ� ������ ��
    private float halfPieceAngleWithPaddings;	// ���� ���⸦ ����� Padding�� ���Ե� ���� ũ��

    public int spinDuration;
    public Transform spnningRoulette;
    public AnimationCurve spnningCurve;

    public int selectIndex;


    private void Awake()
    {
        pieceAngle = 360 / roulettePieceDatas.Length;
		halfPieceAngle = pieceAngle * 0.5f;
        halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle * 0.25f);

        SpawnPieceAndLine();
    }

    private void SpawnPieceAndLine()
    {
        for (int i = 0; i < roulettePieceDatas.Length; ++i)
        {
            Transform piece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity, pieceParent);
            piece.GetComponent<RoulettePiece>().SetUp(roulettePieceDatas[i]);
            piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * i));
       //     piece.localPosition = new Vector2

            Transform line = Instantiate(linePrefab, lineParent.position, Quaternion.identity, lineParent);
            line.RotateAround(lineParent.position, Vector3.back, (pieceAngle * i) + halfPieceAngle);
        }
    }
    private int GetRandomIndex()
    {
        int randomIdx = Random.Range(0, roulettePieceDatas.Length);
        return randomIdx;
    }
    public void Spin(UnityAction<RoulettePieceData> action = null)
    {
        // �귿�� ��� �� ����
        selectIndex = GetRandomIndex();
        // ���õ� ����� �߽� ����
        float angle = pieceAngle * selectIndex;
        // ��Ȯ�� �߽��� �ƴ� ��� �� ���� ���� ������ ���� ����
        float leftOffset = (angle - halfPieceAngleWithPaddings) % 360;
        float rightOffset = (angle + halfPieceAngleWithPaddings) % 360;
        float randomAngle = Random.Range(leftOffset, rightOffset);

        // ��ǥ ����(targetAngle) = ��� ���� + 360 * ȸ�� �ð� * ȸ�� �ӵ�
        int rotateSpeed = 2;
        float targetAngle = (randomAngle + 360 * spinDuration * rotateSpeed);

        Debug.Log($"SelectedIndex:{selectIndex}, Angle:{angle}");
        Debug.Log($"left/right/random:{leftOffset}/{rightOffset}/{randomAngle}");
        Debug.Log($"targetAngle:{targetAngle}");

        StartCoroutine(OnSpin(targetAngle, action));
    }

    private IEnumerator OnSpin(float end, UnityAction<RoulettePieceData> action)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / spinDuration;

            float z = Mathf.Lerp(0, end, spnningCurve.Evaluate(percent));
            spnningRoulette.rotation = Quaternion.Euler(0, 0, z);

            yield return null;
        }


        if (action != null) action.Invoke(roulettePieceDatas[selectIndex]);
    }

}
