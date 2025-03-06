var canvas = document.getElementById('game');
var context = canvas.getContext('2d');

// Tamaño de la cuadrícula
var grid = 16;
var count = 0;
var score = 0; // Puntaje

var snake = {
    x: 160,
    y: 160,
    dx: grid,
    dy: 0,
    cells: [],
    maxCells: 4
};

var apple = {
    x: 320,
    y: 320
};

// Función para obtener números aleatorios
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}

// Función para guardar el puntaje en el servidor
function saveScore(username, score) {
    fetch('/GameHistory/SaveScore', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            username: username,
            score: score
        })
    })
        .then(response => response.json())
        .then(data => {
            console.log('Score saved successfully:', data);
            document.location.reload(); // Recargar la página para reiniciar el juego
        })
        .catch(error => console.error('Error saving score:', error));
}

// Loop del juego
function loop() {
    requestAnimationFrame(loop);

    if (++count < 4) return;

    count = 0;
    context.clearRect(0, 0, canvas.width, canvas.height);

    snake.x += snake.dx;
    snake.y += snake.dy;

    // Si la serpiente choca, termina el juego
    if (snake.x < 0 || snake.x >= canvas.width || snake.y < 0 || snake.y >= canvas.height) {
        let username = ("player");
        if (username) {
            saveScore(username, score);
        } else {
            document.location.reload();
        }
        return;
    }

    snake.cells.unshift({ x: snake.x, y: snake.y });

    if (snake.cells.length > snake.maxCells) {
        snake.cells.pop();
    }

    context.fillStyle = 'red';
    context.fillRect(apple.x, apple.y, grid - 1, grid - 1);

    context.fillStyle = 'green';
    snake.cells.forEach(function (cell, index) {
        context.fillRect(cell.x, cell.y, grid - 1, grid - 1);

        if (cell.x === apple.x && cell.y === apple.y) {
            snake.maxCells++;
            score++;
            apple.x = getRandomInt(0, 25) * grid;
            apple.y = getRandomInt(0, 25) * grid;
        }

        for (var i = index + 1; i < snake.cells.length; i++) {
            if (cell.x === snake.cells[i].x && cell.y === snake.cells[i].y) {
                let username = ("player");
                if (username) {
                    saveScore(username, score);
                } else {
                    document.location.reload();
                }
                return;
            }
        }
    });

    context.fillStyle = 'white';
    context.font = '20px Arial';
    context.fillText('Score: ' + score, 10, 20);
}

// Escuchar eventos de teclado
document.addEventListener('keydown', function (e) {
    if (e.which === 37 && snake.dx === 0) { snake.dx = -grid; snake.dy = 0; }
    else if (e.which === 38 && snake.dy === 0) { snake.dy = -grid; snake.dx = 0; }
    else if (e.which === 39 && snake.dx === 0) { snake.dx = grid; snake.dy = 0; }
    else if (e.which === 40 && snake.dy === 0) { snake.dy = grid; snake.dx = 0; }
});

// Iniciar el juego
requestAnimationFrame(loop);
