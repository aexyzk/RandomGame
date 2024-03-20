import pygame

pygame.init()
screen = pygame.display.set_mode((640, 480))
clock = pygame.time.Clock()
running = True
dt = 0
movment_speed = 15
movement_mutipler = 1

player_pos = pygame.Vector2(screen.get_width() / 2, screen.get_height() / 2)

while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

    screen.fill("purple")

    pygame.draw.circle(screen, "red", player_pos, 40)

    keys = pygame.key.get_pressed()
    vertical = keys[pygame.K_s] - keys[pygame.K_w]
    horizantal = keys[pygame.K_d] - keys[pygame.K_a]

    if vertical and horizantal:
        movement_mutipler = .707107
    else:
        movement_mutipler = 1

    player_pos = pygame.Vector2(player_pos.x + (horizantal * movment_speed * movement_mutipler), player_pos.y + (vertical * movment_speed * movement_mutipler))

    pygame.display.flip()

    dt = clock.tick(60) / 1000

pygame.quit()