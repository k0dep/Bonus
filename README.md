# Bonus
 Мобильная игра на unity в жанре M3
 
 ![Скриншот](https://raw.githubusercontent.com/k0dep/Bonus/master/Img/Screen.png)
 
## Перемещение активного кристала
 Для перемещения кативного кристала нужно тапнуть на ту сторону экрана в которую нужно сдвинуть кристала
 
## Архитектура игры
 Игра использует HMVC(иерархтический модель-вид-контроллер) в качестве базового архитектурного решения.
 
 Иерархия контроллеров выглядит так:
```
   + GameInputController
   |
   |-+ GameController
   | |
   | |-+ FieldController
   | | 
   | |-+ EntityController
   | 
   |-+ BonusController
   |
   + AudioController
```

## Используемые технологии
 * Zenject - IOC-контейнер
 * UniRx - Rx и мультипоточность
 * Poster - для обмена сообщениями