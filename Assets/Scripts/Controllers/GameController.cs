using Messages;
using Models;
using Poster;
using Services;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class GameController : IInitializable, IGameController
    {
        public readonly IBonusController BonusController;
        public readonly IFieldController FieldController;
        public readonly IFieldBoundsApplyService FluidFiledApplyService;
        public readonly GameConfigModel GameConfigModel;
        public readonly IMessageBinder MessageBinder;

        private bool isPause;

        public GameController(IBonusController bonusController,
            IFieldController fieldController,
            IMessageBinder messageBinder,
            IFieldBoundsApplyService fluidFiledApplyService,
            GameConfigModel gameConfigModel)
        {
            BonusController = bonusController;
            FieldController = fieldController;
            FluidFiledApplyService = fluidFiledApplyService;
            GameConfigModel = gameConfigModel;
            MessageBinder = messageBinder;
        }

        public void Initialize()
        {
            FluidFiledApplyService.Apply();
            FieldController.Initialize();
            FieldController.SpawnEntities();

            Observable
                .Interval(TimeSpan.FromSeconds(GameConfigModel.UpdateInterval))
                .Subscribe(_ => UpdateLogic());

            Observable
                .Interval(TimeSpan.FromSeconds(GameConfigModel.SpawnInterval))
                .Subscribe(_ => UpdateSpawn());

            MessageBinder.Bind<EntityFireMessage>(msg => BonusController.EntityFired(msg.Entity));
        }

        public void Reset()
        {
            FieldController.Reset();
            Time.timeScale = 1;
        }

        public void Pause()
        {
            isPause = true;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            isPause = false;
            Time.timeScale = 1;
        }

        public void SlideLeft()
        {
            FieldController.MoveEntity(false);
        }

        public void SlideRight()
        {
            FieldController.MoveEntity(true);
        }

        public void UpdateLogic()
        {
            if (isPause)
            {
                return;
            }

            FieldController.FireEntities();
            FieldController.FallEntities();
            BonusController.SelectRandomEntityBonus();
        }

        public void UpdateSpawn()
        {
            if (isPause)
            {
                return;
            }

            FieldController.SpawnEntities();
        }
    }
}