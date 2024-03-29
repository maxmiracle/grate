﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    /// <summary>
    /// Концепция четвертой версии. 
    /// Ключевое слово этой версии - композиция.
    /// Еще в третьей версии я пришел к выводу, что на выходе должна быть композиция. 
    /// Критерии должны подбираться с учетом результата. То есть должна быть не детерминированная связь с выходом.
    /// Это должен быть интерактивный процесс определения объекта. Количество объектов должно быть не более MaxObjects.
    /// Если же количество объектов больше, то нужно выделить самые яркие из них. 
    /// Если объекты равнозначны по критериям, то нужно выбрать максимальное количество и сузить область рассмотрения. Или другими словами, 
    /// наложить фильтр приоритетности рассмотрения объектов или фокусировки, например, на центральной части. 
    /// Или те, которые первые по каким то критериям. 
    /// Eсли есть матрица N*N элементов. То можно сначала выделить 1,1 1,2 1,3 и все остальные.
    /// Композиция очень важна. Чтобы работать с объектами их должно быть немного. Иначе невозможно сравнивать и анализировать все взаимосвязи.
    /// Таким образом, нужно очерчивать области рассмотрения или разбивать их на части.
    /// Можно будет уточнить границы на более поздних этапах детализации внутренней структуры областей.
    /// fruition = осуществление
    /// earnest = серьезный
    /// </summary>


    public class ModelParameters
    {
        /// <summary>
        /// Min top level objects. Composed objects at top level.
        /// At least one object should be found at top level.
        /// </summary>
        public uint MinObjects = 1;


        /// <summary>
        /// Max top level objects. Composed objects at top level.
        /// Max objects should be greater than 1 and less then 10.
        /// By default the value of MaxObjects is chosen as 7 according to psycology.
        /// </summary>
        public uint MaxObjects = 7;

        /// <summary>
        /// Criteria - a set of criterion(Measure,Assess)
        /// </summary>
        public IEnumerable<Criterion<Model, Composition>> Criteria = new Criterion<Model, Composition>[] {
              // Criterion 1
              new Criterion<Model, Composition>
              {
                  Measure = CompositionFullnessMeasurement.compositionFullnessMeasurement,
                  Assessment = new ThresholdAssessment(){Threshold = 90 }.Assess
              }
        };

    }
}
