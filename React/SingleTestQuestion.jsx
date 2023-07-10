import React from "react";
import { Field, FieldArray, ErrorMessage, useFormikContext } from "formik";
import debug from "sabio-debug";

const _logger = debug.extend("SingleTestQuestion");
function SingleTestQuestion() {
  const context = useFormikContext();
  _logger("contezt in singletestquestion", context);
  const renderTestQuestions = ({ form, push }) => {
    const values = form.values;
    const mapTestQuestion = (question, index) => {
      const onAdd = () =>
        push({
          id: 0,
          question: "",
          testAnswers: [{ id: index + 1, answer: "" }],
        });
      return (
        <div className="row" key={index} style={{ padding: 10 }}>
          <div className="col">
            <label htmlFor="testQuestion" className="Q">
              Q{index + 1}
            </label>
            <Field
              type="text"
              name={`testQuestions.${index}.question`}
              placeholder="Enter your question"
              className="field"
            />
          </div>
          <div className="row">
            <FieldArray
              name={`testQuestions.${index}.testAnswers`}
              render={({ push, remove }) => {
                _logger("fieldarray", push, remove);
                const mapTestAnswer = (answer, aIndex) => {
                  const onAdd = () => push({ id: index + 1, answer: "" });
                  return (
                    <div
                      className="row"
                      key={`Answer: ${aIndex}`}
                      style={{ padding: 10 }}
                    >
                      <div className="col">
                        <Field
                          type="text"
                          name={`testQuestions.${index}.testAnswers.${aIndex}.answer`}
                          className="field"
                          placeholder="Enter an answer choice"
                        />
                        <ErrorMessage
                          name={`testQuestions.${index}.testAnswers.${aIndex}.answer`}
                          component="div"
                          className="has-error"
                        />
                      </div>
                      <div className="col">
                        <button
                          type="button"
                          onClick={onAdd}
                          className="button"
                        >
                          +
                        </button>

                        <button
                          type="button"
                          className="button"
                          onClick={() => {
                            remove(index);
                          }}
                        >
                          -
                        </button>
                      </div>
                    </div>
                  );
                };
                return (
                  <div>
                    {values.testQuestions[index].testAnswers.map(mapTestAnswer)}
                  </div>
                );
              }}
            ></FieldArray>
          </div>
          <button
            type="button"
            id="addQ"
            className="btn btn-success"
            onClick={onAdd}
          >
            + Next Question
          </button>
        </div>
      );
    };
    return <div>{values.testQuestions.map(mapTestQuestion)}</div>;
  };
  return (
    <FieldArray name="testQuestions" render={renderTestQuestions}></FieldArray>
  );
}

export default SingleTestQuestion;
