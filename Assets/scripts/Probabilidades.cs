using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Probabilidades {

    public static int prob_objeto; // probabilidad que caiga un objeto al destruir un bloque
    public static int prob_bueno; // probabailidad que el objeto sea "bueno"
    // objetos buenos
    public const int SUMAVIDA = 0;
    public const int AGRANDAR = 1;
    public const int PROYECTILES = 2;
    public const int SUELOEXTRA = 3;
    public const int IMAN = 4;
    public static int[] prob_objs_buenos = new int[5]; // orden: sumaVida, agrandar, proyectiles, sueloExtra, iman
    public static int[] delimitaciones_buenos = new int[4];
    // objetos malos
    public const int RESTAVIDA = 0;
    public const int ACHICAR = 1;
    public const int DISLEXIA = 2;
    public const int TORTUGA = 3;
    public static int[] prob_objs_malos = new int[4]; // orden: restaVida, achicar, dislexia, tortuga
    public static int[] delimitaciones_malos = new int[3];

    public static void inicializarProbabilidades()
    {
        Probabilidades.prob_objeto = 40;

        if (Opciones.dificultad == 0)
        { // facil
            Vidas.vidas = 5;
            // probs favorables
            Probabilidades.prob_bueno = 60;
            Probabilidades.inicializarProbsBuenos(new int[] { 9, 26, 19, 25, 21 });
            Probabilidades.inicializarProbsMalos(new int[] { 32, 36, 14, 18 });
        }
        else if (Opciones.dificultad == 1)
        { // normal
            Vidas.vidas = 3;
            // probabilidades equitativas
            Probabilidades.prob_bueno = 50;
            Probabilidades.inicializarProbsBuenos(new int[] { 23, 26, 13, 23, 15 });
            Probabilidades.inicializarProbsMalos(new int[] { 28, 32, 18, 22 });
        }
        else
        { // dificil
            Vidas.vidas = 1;
            //probs desfavorables
            Probabilidades.prob_bueno = 40;
            Probabilidades.inicializarProbsBuenos(new int[] { 25, 30, 9, 25, 11 });
            Probabilidades.inicializarProbsMalos(new int[] { 9, 34, 26, 31 });
        }
    }

    public static void inicializarProbsBuenos(int[] probs) { 
        prob_objs_buenos[SUMAVIDA] = probs[SUMAVIDA];
        prob_objs_buenos[AGRANDAR] = probs[AGRANDAR];
        prob_objs_buenos[PROYECTILES] = probs[PROYECTILES];
        prob_objs_buenos[SUELOEXTRA] = probs[SUELOEXTRA];
        prob_objs_buenos[IMAN] = probs[IMAN];

        establecerDelimitacionesBuenos(prob_objs_buenos);
    }

    private static void establecerDelimitacionesBuenos(int[] probs) { 
        delimitaciones_buenos[0] = probs[0];
        delimitaciones_buenos[1] = delimitaciones_buenos[0] + probs[1];
        delimitaciones_buenos[2] = delimitaciones_buenos[1] + probs[2];
        delimitaciones_buenos[3] = delimitaciones_buenos[2] + probs[3];
    }

    public static void cambiarProbsObjeto(bool esBueno, int objeto, int variacion) {
        if (esBueno) {
            for (int i = 0; i < prob_objs_buenos.Length; i++) {
                if (i == objeto) {
                    prob_objs_buenos[i] += variacion;
                }
                else {
                    prob_objs_buenos[i] += (-variacion) / (prob_objs_buenos.Length - 1);
                }
            }
            establecerDelimitacionesBuenos(prob_objs_buenos);
        }
        else {
            for (int i = 0; i < prob_objs_malos.Length; i++) {
                if (i == objeto) {
                    prob_objs_malos[i] += variacion;
                }
                else {
                    prob_objs_malos[i] += (-variacion) / (prob_objs_malos.Length - 1);
                }
            }
            establecerDelimitacionesMalos(prob_objs_malos);
        }
    }

    public static void inicializarProbsMalos(int[] probs) {
        prob_objs_malos[RESTAVIDA] = probs[RESTAVIDA];
        prob_objs_malos[ACHICAR] = probs[ACHICAR];
        prob_objs_malos[DISLEXIA] = probs[DISLEXIA];
        prob_objs_malos[TORTUGA] = probs[TORTUGA];

        establecerDelimitacionesMalos(prob_objs_malos);
    }

    private static void establecerDelimitacionesMalos(int[] probs) {
        delimitaciones_malos[0] = probs[0];
        delimitaciones_malos[1] = delimitaciones_malos[0] + probs[1];
        delimitaciones_malos[2] = delimitaciones_malos[1] + probs[2];
    }
	
}
