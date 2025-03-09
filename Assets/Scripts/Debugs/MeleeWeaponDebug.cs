using UnityEngine;

namespace Assets.Scripts.Debugs
{
    public class MeleeWeaponDebug : MonoBehaviour
    {
        // Disse v�rdier kan du s�tte i Inspector eller via din WeaponSO-data
        public Vector3 boxSize = new(1, 1, 1);
        public float range = 1f;
        public float offset = 1.1f; // Afstand fra spilleren hvor casten starter
        public Color debugColor = Color.green;

        private void OnDrawGizmosSelected()
        {
            // Antag at dette script er knyttet til spilleren (eller den GameObject, der repr�senterer angrebsudgangspunktet)
            Transform t = transform;
            Vector3 startPos = t.position + t.forward * offset;

            // Beregn center for boksen: midtpunktet af casten (startPos + halvdelen af range i fremad)
            Vector3 center = startPos + t.forward * (range * 0.5f);

            // S�t Gizmos.matrix til at rotere boksen, s� den f�lger din GameObjects rotation
            Gizmos.color = debugColor;
            Gizmos.matrix = Matrix4x4.TRS(center, t.rotation, boxSize);

            // Tegn en enhedskube, der via matrixen skaleres til din boxSize
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}