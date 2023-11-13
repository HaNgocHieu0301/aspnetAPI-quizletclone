import React, {
  useState,
  useEffect,
  useRef,
  forwardRef,
  useImperativeHandle,
} from "react";
import IconSvg from "../../components/IconSvg";
import Icons from "~/assets/icons";
import { Button, Flex, Input, Form } from "antd";
import TermRow from "../../components/Flashcard/TermRow";
import ImportModal from "../../components/Flashcard/ImportModal";
import axios from "axios";
import { Answer } from "~/types/Answer";
import { Flashcard } from "~/types/FlashCard";
import { RemoveQuestion } from "~/components/Flashcard/QuestionFunctions";
import { AddLesson } from "~/types/AddLesson";
import { jwtDecode } from "jwt-decode";

const CreateSet = () => {
  const [isFocusTitle, setIsFocusTitle] = useState(false);
  const [isFocusDescription, setIsFocusDescription] = useState(false);
  const [isOpenImportModal, setIsOpenImportModal] = useState(false);
  const [flashcards, setFlashcards] = useState<Flashcard[]>([]); // [ [word, definition], [word, definition
  const [lessonTitle, setLessonTitle] = useState("");
  const [lessonDescription, setLessonDescription] = useState("");
  const flashcardListRef = useRef<any>(null);
  const flashcardFormRef = useRef<any>(null);

  useEffect(() => {
    for (let index = 0; index < 3; index++) {
      setFlashcards((flashcards) => [
        ...flashcards,
        {
          learningStatusId: 1,
          lessonId: 0,
          questionId: 0,
          term: "",
          answers: [{ definition: "", answerId: 0, image: "", questionId: 0 }],
          isStarred: false,
        },
      ]);
    }
  }, []);

  const importedCallback = (lst: Flashcard[]) => {
    const tmp = flashcards.concat(
      lst.map((flashcard) => ({
        ...flashcard,
        questionId: 0,
        lessonId: 0,
        answers: flashcard.answers.map((answer) => ({
          ...answer,
          answerId: 0,
          questionId: 0,
          image: "",
        })),
      }))
    );
    setIsOpenImportModal(false);
    setFlashcards(tmp);
  };

  const swapTermAndDefinitionHandler = () => {
    // console.log("swap");
    // const tmp = flashcards.map((flashcard) => {
    //   // for (let i = 1; i < flashcard.answers.length; i++) {
    //   //   RemoveAnswerInQuestion(flashcard.answers[i].answerId, flashcard);
    //   // }
    //   const tmp2: Flashcard = {
    //     ...flashcard,
    //     term: flashcard.answers.map((answer) => answer.definition).join("\n"),
    //     answers: [
    //       {
    //         answerId: flashcard.answers[0].answerId,
    //         questionId: flashcard.questionId,
    //         definition: flashcard.term,
    //         image: "",
    //       },
    //     ],
    //   };
    //   return tmp2;
    // });
    // console.log(tmp);
    // setFlashcards(tmp);
  };

  const addNewLineHandler = () => {
    const fl: Flashcard = {
      learningStatusId: 1,
      lessonId: 0,
      questionId: 0,
      term: "",
      answers: [{ definition: "", answerId: 0, image: "", questionId: 0 }],
      isStarred: false,
    };

    let tmp = [...flashcards, fl];
    setFlashcards(tmp);
  };

  const removeCallback = (index: number) => {
    flashcards.splice(index, 1);
    setFlashcards((flashcards) => [...flashcards]);
  };

  const updateCallback = (index: number, updatedFields: any) => {
    flashcards[index] = {
      ...updatedFields,
    };
    setFlashcards((flashcards) => [...flashcards]);
  };

  const handleSubmitForm = async () => {
    try {
      const data: Flashcard[] = flashcardListRef.current.getValue();
      console.log(data);
      const Questions: Flashcard[] = [];
      data.forEach((element: Flashcard) => {
        Questions.push({
          questionId: element.questionId,
          learningStatusId: element.learningStatusId,
          term: element.term,
          isStarred: element.isStarred,
          lessonId: 0,
          answers: element.answers,
        });
      });
      const Lesson: AddLesson = {
        title: lessonTitle,
        description: lessonDescription,
        createAt: new Date(),
        modifiedAt: new Date(),
        visibleId: 1,
        folderId: 1,
        rate: 0,
        userId: jwtDecode(localStorage.getItem("token") || "").sub || "",
        questions: Questions,
      };
      const lessonJson = JSON.stringify(Lesson);
      console.log(lessonJson);
      const response = await axios.post(
        "http://localhost:5219/api/Lessons/AddLessonWithQuestions",
        lessonJson,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log(response);
      if (response.status >= 200 && response.status < 300) {
        // window.location.href = "/lesson/" + lessonIdNum;
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <main className="w-full">
        <div className="CreateSetPage">
          <div className="px-20 py-8">
            <Flex className="mb-4" align="center" justify="space-between">
              <h1>Create a new study set</h1>
              <Button size="large" type="primary" onClick={handleSubmitForm}>
                Create
              </Button>
            </Flex>
            {/* header with title and description input */}
            <Flex
              vertical
              gap={"small"}
              className="UIContainer mx-auto max-w-[81.25em]"
            >
              <Flex vertical gap={"small"} className="w-[50%]">
                <Flex vertical align="flex-start" className="max-w-[36.75rem]">
                  <div className="w-full">
                    <Input
                      value={lessonTitle}
                      bordered={false}
                      className="p-0"
                      onFocus={() => setIsFocusTitle(true)}
                      onBlur={() => setIsFocusTitle(false)}
                      onChange={(e) => setLessonTitle(e.target.value)}
                    />
                    <hr
                      className={
                        isFocusTitle ? "border-[#ffcd1f]" : "border-black"
                      }
                    />
                    <Flex align="flex-start" className="">
                      <div className="text-[#939bb4] font-semibold text-[0.75rem] uppercase border-t-2 border-[#ffcd1f]">
                        Title
                      </div>
                    </Flex>
                  </div>
                </Flex>
              </Flex>
              <Flex vertical gap={"small"} className="w-[50%]">
                <Flex vertical align="flex-start" className="max-w-[36.75rem]">
                  <div className="w-full">
                    <Input
                      value={lessonDescription}
                      bordered={false}
                      className="p-0"
                      onFocus={() => setIsFocusDescription(true)}
                      onBlur={() => setIsFocusDescription(false)}
                      onChange={(e) => setLessonDescription(e.target.value)}
                    />
                    <hr
                      className={
                        isFocusDescription ? "border-[#ffcd1f]" : "border-black"
                      }
                    />
                    <Flex align="flex-start" className="">
                      <div className="text-[#939bb4] font-semibold text-[0.75rem] uppercase border-t-2 border-[#ffcd1f]">
                        Description
                      </div>
                    </Flex>
                  </div>
                </Flex>
              </Flex>
            </Flex>

            {/* options button */}
            <Flex
              justify="space-between"
              className="UIContainer mx-auto max-w-[81.25em] mt-5"
            >
              <Button
                className="flex items-center"
                size="large"
                icon={<IconSvg width={16} height={16} iconName="plus" />}
                onClick={() => setIsOpenImportModal(true)}
              >
                Import
              </Button>
              <Flex gap={"middle"}>
                <Button
                  size="large"
                  shape="circle"
                  className="flex flex-row justify-center items-center"
                  icon={<IconSvg width={16} height={16} iconName="settings" />}
                />
                {/* swap term and definition button */}
                <Button
                  shape="circle"
                  size="large"
                  className="flex flex-row justify-center items-center"
                  icon={
                    <IconSvg
                      width={16}
                      height={16}
                      iconName="swap-horizontal"
                    />
                  }
                  onClick={swapTermAndDefinitionHandler}
                />
              </Flex>
            </Flex>
          </div>

          <div className="MainContent">
            <Flex className="UIContainer mx-auto max-w-[81.25em] px-10">
              <div className="py-6">
                <div className="StudiableItems">
                  <Form ref={flashcardFormRef}>
                    <Form.Item name="flashcards">
                      <FlashcardControl
                        ref={flashcardListRef}
                        flashcards={flashcards}
                        removeCallback={removeCallback}
                        updateCallback={updateCallback}
                      />
                    </Form.Item>
                  </Form>
                </div>
              </div>
            </Flex>
          </div>
          {/* Add new row */}
          <div className="UIContainer mx-auto max-w-[81.25em] px-10">
            <div className="py-6">
              <div className="StudiableItems text-center">
                <button
                  className="bg-white border-t-0 border-l-0 border-r-0 cursor-pointer font-bold border-b-4 border-b-[#3ccfcf] hover:text-[#ffcd1f] hover:border-b-yellow-400 py-2 px-0"
                  onClick={addNewLineHandler}
                >
                  <span>+ Thêm Thẻ</span>
                </button>
              </div>
            </div>
          </div>
        </div>
        <ImportModal
          isOpen={isOpenImportModal}
          closeHandler={() => setIsOpenImportModal(false)}
          importedCallback={importedCallback}
        />
      </main>
      <Icons />
    </>
  );
};
const FlashcardControl = forwardRef<any, any>(
  ({ flashcards, removeCallback, updateCallback }, ref) => {
    const [localFlashcards, setLocalFlashcards] =
      useState<Flashcard[]>(flashcards);
    useEffect(() => {
      console.log("FlashcardControlEffect");
      console.log(flashcards);
      setLocalFlashcards(flashcards);
      console.log(localFlashcards);
    }, [flashcards]);

    useImperativeHandle(ref, () => ({
      getValue: () => {
        return localFlashcards;
      },
    }));

    const removeFlashcardCallback = (index: number) => {
      // localFlashcards.splice(index, 1);
      // setLocalFlashcards((localFlashcards) => [...localFlashcards]);
      console.log(localFlashcards);

      removeCallback(index);
    };

    const updateFlashcard = (index: number, updatedFields: any) => {
      // localFlashcards[index] = {
      //   ...updatedFields,
      // };
      // console.log(localFlashcards[index]);
      // setLocalFlashcards((localFlashcards) => [...localFlashcards]);
      updateCallback(index, updatedFields);
    };
    return (
      <>
        {localFlashcards.map((flashcard: Flashcard, index: number) => (
          <TermRow
            key={index}
            index={index + 1}
            flashcard={flashcard}
            removeCallback={() => removeFlashcardCallback(index)}
            updateCallback={(updatedFields: any) =>
              updateFlashcard(index, updatedFields)
            }
          />
        ))}
      </>
    );
  }
);
export default CreateSet;
