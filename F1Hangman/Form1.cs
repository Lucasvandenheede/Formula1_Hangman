using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace F1Hangman
{
    public partial class Form1 : Form
    {
        private string correctWord;
        private string hiddenWord;
        private int incorrectGuesses;
        private List<string> Guesses = new List<string>();

        public Form1()
        {
            InitializeComponent();
            generateHiddenWord();
        }

        private void generateHiddenWord() // Function to generate a hidden word
        {
            // Connection to database
            SQLiteConnection connection = new SQLiteConnection(@"data source=C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Database\F1woorden.db");

            // Open connection
            connection.Open();

            // Create command object
            string query = "SELECT * FROM Formula1";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);

            // Execute query and get data reader
            SQLiteDataReader reader = cmd.ExecuteReader();

            // Create a list to store the words
            List<string> words = new List<string>();

            // Read all the words from the data reader and add them to the list
            while (reader.Read())
            {
                words.Add(reader["woorden"].ToString());
            }

            // Close the data reader and connection
            reader.Close();
            connection.Close();

            // Create the hidden word from the database
            int randomIndex = new Random().Next(0, words.Count);
            correctWord = words[randomIndex].ToLower();
            hiddenWord = "";

            // Replacing each letter with "_"
            for (int i = 0; i < correctWord.Length; i++)
            {
                char currentChar = words[randomIndex][i];
                if (char.IsWhiteSpace(currentChar))
                {
                    hiddenWord += ' ';
                }
                else
                {
                    hiddenWord += '.';
                }
            }
            // Displaying hidden word
            label2.Text = hiddenWord;
            label1.Text = correctWord; // Showing for testing purpose

            // Set incorrect guesses to 0
            incorrectGuesses = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Guess button

            // Retrieves the user's guess from the text box and set it to lowercase
            String guess = textBox1.Text.ToLower();

            // Validate the user's guess
            if (guess == correctWord)
            {
                label2.Text = correctWord;
                pictureBox1.Image = new Bitmap(@"C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Images\win.png");
                MessageBox.Show("Congratulations! You have won the Hangman Grand Prix!");
                textBox1.Text = "";
            }
            else if (Guesses.Contains(guess)) // If player enters letter already guessed
            {
                MessageBox.Show("You've already guessed this letter");
                textBox1.Text = "";
            }
            else if (correctWord.Contains(guess) && guess.Length == 1) // Check if the guess is correct
            {
                // If the guess is correct
                for (int i = 0; i < correctWord.Length; i++)
                {
                    if (correctWord[i].ToString() == guess)
                    {
                        // Replace the underscore at the correct position with the guessed letter
                        label2.Text = label2.Text.Substring(0, i) + guess + label2.Text.Substring(i + 1);
                    }
                }

                // Check for win condition
                if (label2.Text == correctWord)
                {
                    pictureBox1.Image = new Bitmap(@"C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Images\win.png");
                    MessageBox.Show("Congratulations! You have won the Hangman Grand Prix!");
                    label2.Text = correctWord;
                }
                textBox1.Text = "";

                // Add the guess to the list of guesses
                Guesses.Add(guess);
            }
            else
            {
                if (!Guesses.Contains(guess) || guess != correctWord) // If guess is not correct
                {
                    // If the guess is incorrect, update the incorrect guesses counter
                    incorrectGuesses++;

                    // Show message if guess is incorrect
                    if (incorrectGuesses < 5)
                    {
                        MessageBox.Show($"The stewards are watching closely. You have {5 - incorrectGuesses} track limits left before you're black-flagged.");
                    }

                    // Reset the textbox
                    textBox1.Text = "";

                    // Update the picture of the hangman
                    if (incorrectGuesses == 1)
                    {
                        pictureBox1.Image = new Bitmap(@"C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Images\1.png");
                    }
                    else if (incorrectGuesses == 2)
                    {
                        pictureBox1.Image = new Bitmap(@"C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Images\2.png");
                    }
                    else if (incorrectGuesses == 3)
                    {
                        pictureBox1.Image = new Bitmap(@"C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Images\3.png");
                    }
                    else if (incorrectGuesses == 4)
                    {
                        pictureBox1.Image = new Bitmap(@"C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Images\4.png");
                    }
                    else if (incorrectGuesses == 5)
                    {
                        pictureBox1.Image = new Bitmap(@"C:\Users\lucas\Thomas more\2APPAI02\Devops\CaseStudy\F1Hangman\Images\crash.png");
                        label2.Text = correctWord;
                        MessageBox.Show($"You have pushed too hard, you have have crashed.\nThe word was: {correctWord}");
                    }

                    // Add the incorrect guess to the list of guesses
                    Guesses.Add(guess);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Start new game
            // Reset everything
            textBox1.Text = ""; // Clears the text box
            pictureBox1.Image = null; // Clears the picture bow
            label2.Text = ""; // Clears the hidden word
            Guesses.Clear(); // Clears the list of guesses

            // Generate new word
            generateHiddenWord();
        }
    }
}