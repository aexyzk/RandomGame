import pygame
import data.debug
import os

# Player Class
class Player(pygame.sprite.Sprite):
    def __init__(self):
        super().__init__()
        down_walk0 = pygame.image.load('data/graphics/player/down_walk0.png').convert_alpha()
        down_walk1 = pygame.image.load('data/graphics/player/down_walk1.png').convert_alpha()
        up_walk0 = pygame.image.load('data/graphics/player/up_walk0.png').convert_alpha()
        up_walk1 = pygame.image.load('data/graphics/player/up_walk1.png').convert_alpha()
        right_walk0 = pygame.image.load('data/graphics/player/right_walk0.png').convert_alpha()
        right_walk1 = pygame.image.load('data/graphics/player/right_walk1.png').convert_alpha()
        left_walk0 = pygame.image.load('data/graphics/player/left_walk0.png').convert_alpha()
        left_walk1 = pygame.image.load('data/graphics/player/left_walk1.png').convert_alpha()
        self.down_walk = [down_walk0, down_walk1]
        self.up_walk = [up_walk0, up_walk1]
        self.right_walk = [right_walk0, right_walk1]
        self.left_walk = [left_walk0, left_walk1]
        self.frame_index = 0
        self.image = self.down_walk[self.frame_index]
        self.rect = self.image.get_rect(midbottom = (SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2))
        self.vertical = 0
        self.horizantal = 0
        self.movement_mutipler = 1
        self.player_speed = 15
        self.old_dir = pygame.Vector2(0,1)

    def update(self):
        self.input()
        self.move_player()
        self.animate()

    def move_player(self):
        hsp = (self.horizantal * self.player_speed) * self.movement_mutipler
        vsp = (self.vertical * self.player_speed) * self.movement_mutipler
        self.rect.x += hsp
        self.rect.y += vsp

    def input(self):
        keys = pygame.key.get_pressed()
        self.vertical = keys[pygame.K_s] - keys[pygame.K_w]
        self.horizantal = keys[pygame.K_d] - keys[pygame.K_a]
        if self.vertical and self.horizantal:
            self.movement_mutipler = .707107
        else:
            self.movement_mutipler = 1
        if (self.old_dir != (self.horizantal, self.vertical)):
            self.old_dir = (self.horizantal, self.vertical) 

    def animate(self):
        if (self.horizantal != 0 or self.vertical != 0):
            self.frame_index += 0.3
            if (self.frame_index >= len(self.down_walk)):
                self.frame_index = 0
        else:
            self.frame_index = 0

        if (self.old_dir[1] > 0):
            self.image = self.down_walk[int(self.frame_index)]
        elif (self.old_dir[1] < 0):
            self.image = self.up_walk[int(self.frame_index)]        
            
        if (self.old_dir[0] > 0):
            self.image = self.right_walk[int(self.frame_index)]
        elif (self.old_dir[0] < 0):
            self.image = self.left_walk[int(self.frame_index)]

# Game Variables
SCREEN_WIDTH = 640
SCREEN_HEIGHT = 480
FRAMERATE = 60
# Pygame
pygame.init()
screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
clock = pygame.time.Clock()
dt = 0
# Groups
player = pygame.sprite.GroupSingle()
player.add(Player())

# add fluffy tail to char
running = True
while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

    screen.fill("purple")

    player.draw(screen)
    player.update()

    pygame.display.flip()

    dt = clock.tick(FRAMERATE) / 1000

pygame.quit()