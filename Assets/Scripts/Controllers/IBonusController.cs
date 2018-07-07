using Models;

namespace Controllers
{
    public interface IBonusController
    {
        void AssignEntityBonus(IEntityModel model);
        void EntityFired(IEntityModel model);
        void SelectRandomEntityBonus();
    }
}