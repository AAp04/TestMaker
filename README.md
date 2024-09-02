# TestMaker

Making a Exam making and taking application. The users will be teachers that build tests and review results. They can also manage students so that students can login in take a test, and see their results for all tests they have taken.
Teachers can modify a test, create a test, or delete a test. A test will contain different types of questions (multiple choice, true/false, and short answer). Teachers should be able to review all grades for a specific test that they chose.
Students can only login, take a test, or see results of their tests.


Database Tables
1. Users
• Columns: UserID, Username, Password, User Type
2. Tests
• Columns: TestID, Name, Duration, CreatorID
3. Questions
• Columns: QuestionID, TestID, QuestionText, Question Type, CorrectAnswer
4. Options (for multiple choice questions)
• Columns: OptionID, QuestionID, OptionText, IsCorrect
5. StudentAnswers
• Columns: AnswerlD, QuestionID, UserID, AnswerGiven
6. Results
• Columns: ResultID, TestID, UserID, Score
Classes
1. User
﻿﻿Properties: UserID, Username, Password, User Type (Teacher/Student)
﻿﻿Methods: LoginQ
2. Teacher: User
• Methods: CreateTest), ModifyTestO, DeleteTest, ViewResults)
3. Student: User
• Methods: TakeTest, ViewResults)
4. Test
﻿﻿Properties: TestID, Name, Duration, CreatorID
﻿﻿Methods: AddQuestion), RemoveQuestionO
5. Question
﻿﻿Properties: QuestionID, Text, Type (MCQ, True/False, Short Answer), CorrectAnswer
﻿﻿Methods: AddOption), RemoveOption
6. MCQQuestion: Question
• Properties: Options (List of possible answers)
7. TestResult
﻿﻿Properties: TestResultID, TestID, UserID, Score, AnswersGiven
﻿﻿Methods: CalculateScore()


Forms
1. Login Form
• ﻿﻿Fields for username and password
• ﻿﻿Login buttons for teachers and students
2. Teacher Dashboard
• ﻿﻿Options to create, modify, or delete tests
﻿﻿• View results of students for specific tests
• ﻿﻿Navigate to test creation or test modification forms
3. Test Creation Form
• ﻿﻿Input fields for test name, description, and duration
• ﻿﻿Options to add multiple types of questions (multiple choice, true/false, short answer)
• ﻿﻿Buttons to save the test or cancel
4. Test Modification Form
• ﻿﻿Load an existing test
• ﻿﻿Modify questions, add new questions, or delete existing questions
• ﻿﻿Save changes or revert
5. Student Dashboard
• ﻿﻿List of available tests to take
• ﻿﻿View results of previously taken tests
6. Test Taking Form
• ﻿﻿Display questions sequentially or all at once, depending on design preference
• ﻿﻿Options for submitting answers
• ﻿﻿Timer functionality if tests are timed
7. Results View Form
• ﻿﻿Display test results after submission
• ﻿﻿For teachers: Display all students' results for a chosen test
