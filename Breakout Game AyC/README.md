
# Breakout Game

Juego en la que su dinamica trata de eliminar bloques debido a la interacción de una bola qu rebota contra una "tabla" (otro bloque) manejado por el usuario y que se puede mover de izquierda a derecha.

Este juego fue creado mediante el seguimiento de un tutorial.

## Crear proyecto

Se crea un nuevo proyecto en: Aplicación de windows forms (.NET Framework).

## Estructura inicial

1. Se crea una etiqueta(label) y se perzonaliza, con (name); txtScore.
   
 ![](https://github.com/Camila-Hinestroza/Herramientas-de-Programacion-3/blob/main/Breakout%20Game%20AyC/imagenes/imagen%202.png)


3. Se agrega un pictureBox que funcionará como el jugador, con (name); player.
   
 ![](https://github.com/Camila-Hinestroza/Herramientas-de-Programacion-3/blob/main/Breakout%20Game%20AyC/imagenes/imagen%203.png)

5. Se agrega otro pictureBox que servirá como la pelota, con el (name): ball.

6. Se utilizan más pictureBox para crear los bloques del juego, se organizan en 5 columnas y se duplica dos veces más para generar la estructura de los bloques, en Tag se agregan como "blocks".

7. Se agrega un timer y se crea como evento con el nombre; mainGameTimerEvent, con un intervalo de 20.
   
 ![](https://github.com/Camila-Hinestroza/Herramientas-de-Programacion-3/blob/main/Breakout%20Game%20AyC/imagenes/imagen%204.png)
 
## Inicializar código

1. Se crean tres variables del tipo booleano, llamadas: goLeft, goRight y isGameover y también se crean cuatro variables más de tipo entero llamadas: score, ballx, bally y playerSpeed.

2. Se crea como un metodo privado(private void) llamado setupGame, dentro de este copiamos el siguiente código:

        private void setupGame()
            {
                score = 0;
                ballx = 5;
                bally = 5;
                playerSpeed = 12;
                txtScore.Text = "Score: " + score;

                gameTimer.Start();
            }

3. se creo un bloque constructivo(foreach), y se le agrego unas condiciones a los bloques(blocks), además, se les asigno un color a estos.

        foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "blocks")
                    {
                        x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Nex (256));
                    }
            }


## Funcionalidad

1. Dentro del metodo keysDown se agrega la funcionalidad para los botones de la siguiente forma:

        if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
        if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

2. Y luego lo hacemos para el metodo keysUp

        if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
        if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }


3.  Dentro del metodo mainGameimeEvente se configura la movilidad del jugador en el espacio horizontal.
Izquierda:
    
        if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }

Derecha:

        if (goRight == true && player.Left < 700)
            {
                player.Left += playerSpeed;
            }

### Elemento: Bola

1. Le damos el movimiento a la bola.

        ball.Left += ballx;
        ball.Top += bally;

2. Posterior a esto le damos el limite.

        if (ball.Left < 0 || ball.Left > 775)
            {
                ballx = -ballx;
            }
        if (ball.Top < 0)
            {
                bally = -bally;
            }

3. Interacción de la bola con la barra que mueve el jugador.

        if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                ball.Top = 463;
                bally = rnd.Next(5, 12) * -1;
                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }
            }

4. Ahora agregamos la interacción con los bloques.

        foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if(ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;
                        this.Controls.Remove(x);
                    }
                }
            }

Toda ésta sección se agrego dentro del metodo mainGameTimerEvent.

### Score y GameOver

1. Ahora configuramos el conteo del puntaje dentro del metodo mainGameTimerEvent.

        txtScore.Text = "Score: " + score;

2. Se crea un metodo llamado gameOver de tipo void privado.

        private void gameOver(string message)
            {
                isGameOver = true;
                gameTimer.Stop();
                txtScore.Text = "Score: " + score + " " + message;
            }

3. Establecemos el limite del puntaje y con esto podemos establecer cuando se gana o se pierde.

        if (score == 15)
            {
                gameOver("¡Ganaste! Presiona Enter para jugar otra vez");
            }
        if (ball.Top > 580)
            {
                gameOver("¡Perdiste! Presiona Enter para intertar otra vez");
            }

### Bloques 

1. Se crea un metodo para darle un lugar a los bloques y los configura.

        private void PlaceBlocks()
            {
                blockArray = new PictureBox[15];
                int a = 0;
                int top = 50;
                int left = 100;
                for(int i = 0; i < blockArray.Length; i++)
                {
                    blockArray[i] = new PictureBox();
                    blockArray[i].Height = 32;
                    blockArray[i].Width = 100;
                    blockArray[i].Tag = "blocks";
                    blockArray[i].BackColor = Color.White;
                    if(a == 5)
                    {
                        top = top + 50;
                        left = 100;
                        a = 0;
                    }
                    if(a < 5)
                    {
                        a++;
                        blockArray[i].Left = left;
                        blockArray[i].Top = top;
                        this.Controls.Add(blockArray[i]);
                        left = left + 130;
                    }
                }
                setupGame();
            }


2. Ahora se crea un metodo para remover los bloques.

        private void removeBlocks()
            {
                foreach(PictureBox x in blockArray)
                {
                    this.Controls.Remove(x);
                }
            }










