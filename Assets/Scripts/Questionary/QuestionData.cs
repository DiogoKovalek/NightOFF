using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;

[Serializable]
public class Question {
    public String enunciado;
    public String[] alternativas;
    public byte correctIndex = 0;

    public Question() {}

    public Question(String enunciado, String[] alternativas, byte correctIndex) {
        this.enunciado = enunciado;
        this.alternativas = alternativas;
        this.correctIndex = correctIndex;
    }
}

[Serializable]
public class QuestionWrapper {
    public List<Question> perguntas;
}

public static class ListQuestions{
    public static List<Question> listQuestion;

    public static Question ChangeQuestion(int index) {
        Question q = listQuestion[index];
        listQuestion.RemoveAt(index);
        if(listQuestion.Count == 0) listQuestion = null;
        return q;
    }

    public static void ClearList() {
        if(listQuestion == null) return;
        listQuestion.Clear();
        listQuestion = null;
    }
}