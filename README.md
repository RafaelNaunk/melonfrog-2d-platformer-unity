# 🐸 MelonFrog

Jogo de plataforma 2D desenvolvido na **Unity** como projeto da disciplina de **Game Development** (UniFECAF). O jogo coloca o jogador no controle de um sapo aventureiro que atravessa fases de dificuldade crescente, coletando melancias, enfrentando inimigos e, ao final, derrotando um chefão.

---

## 📖 Sobre o jogo

**MelonFrog** é um platformer 2D de estilo *pixel art*, com mecânicas de movimento fluido, três fases progressivas e uma batalha final contra um boss. O projeto foi construído do zero na Unity, com foco em criar uma experiência jogável completa e funcional, adequada como peça de portfólio.

O objetivo do jogador é avançar pelas fases, coletar o máximo de frutas, sobreviver aos inimigos e chegar ao confronto final.

---

## 🎮 Controles

| Ação | Tecla |
|------|-------|
| Mover para a esquerda / direita | Setas ◀ ▶ ou A / D |
| Correr | Segurar **Shift** |
| Pular | **Espaço** |
| Pulo duplo | **Espaço** no ar |
| Escalar | Setas ▲ ▼ ou W / S (sobre escadas) |

---

## ⚙️ Mecânicas implementadas

- **Movimento responsivo**: andar, correr, pular, pulo duplo e escalada, cada ação com sua própria animação.
- **Animações por estado**: o personagem alterna entre idle, corrida, pulo, queda e pulo duplo através do Animator da Unity, controlado por parâmetros de velocidade e contato com o chão.
- **Inimigos com IA de patrulha**: caminham entre limites definidos, detectam bordas e paredes por sensores, e podem ser derrotados ao serem pisados na cabeça.
- **Sistema de chefão (boss)**: inimigo gigante com barra de vida, múltiplos golpes para derrotar, invencibilidade temporária entre golpes (i-frames) e feedback visual de dano.
- **Coletáveis**: frutas espalhadas pelas fases, com feedback visual e sonoro ao serem coletadas.
- **HUD funcional**: contador de frutas e indicador de vidas com ícone de coração.
- **Plataformas atravessáveis (one-way)**: o jogador sobe atravessando por baixo e pousa por cima, usando Platform Effector 2D.
- **Sistema de vidas e respawn**: ao tomar dano ou cair no vazio, o jogador perde uma vida e retorna ao início da fase; ao zerar as vidas, a fase reinicia.
- **Fluxo de telas completo**: menu inicial, três níveis encadeados e tela de vitória.
- **Áudio**: trilha sonora de fundo, efeitos sonoros para ações (pulo, coleta, dano) e música de vitória ao derrotar o boss.

---

## 🗺️ Estrutura das fases

O design segue uma progressão de dificuldade pensada como **ensinar → cobrar → combinar**:

- **Nível 1** — Introduz as mecânicas básicas em um ambiente seguro.
- **Nível 2** — Cobra precisão: saltos maiores, uso obrigatório da escalada e mais inimigos.
- **Nível 3** — Combina todos os desafios e culmina na batalha contra o boss.

---

## 🚀 Como executar

### Opção 1 — Jogar o executável (recomendado)

1. Baixe o arquivo compactado do jogo (`MelonFrog.zip`).
2. Extraia a pasta inteira em um local do seu computador.
3. Abra a pasta extraída e execute o arquivo **`MelonFrog.exe`**.

> ⚠️ Mantenha o `.exe` junto da pasta `MelonFrog_Data` e dos demais arquivos — o jogo não funciona com o executável isolado.

### Opção 2 — Abrir o projeto na Unity

1. Clone ou baixe este repositório.
2. Abra o projeto pela **Unity Hub** (versão **Unity 6.x**).
3. Abra a cena `Assets/Cenas/MenuInicial` e pressione **Play**.

---

## 🛠️ Ferramentas utilizadas

- **Unity 6** — engine de desenvolvimento.
- **C#** — linguagem de programação das mecânicas.
- **Animator da Unity** — sistema de animação por estados.
- **Tilemap** — construção dos cenários.

---

## 🎨 Créditos dos assets

Os recursos gráficos e sonoros utilizados são de repositórios gratuitos:

- **Sprites de personagem, inimigos e cenário**: [Pixel Adventure](https://pixelfrog-assets.itch.io/) por Pixel Frog (itch.io).
- **Efeitos sonoros e trilha**: [Kenney](https://kenney.nl/) (licença CC0) e [Pixabay Music](https://pixabay.com/music/).

---

## 👤 Autor 

Projeto individual desenvolvido para a disciplina de Game Development — UniFECAF.

---

## 📂 Estrutura do repositório

```
├── Assets/              # Scripts (C#), sprites, áudios e cenas
├── ProjectSettings/     # Configurações do projeto Unity
├── Prints/              # Capturas de tela do jogo e das fases
├── MelonFrog.zip        # Build executável (Windows)
└── README.md
```
