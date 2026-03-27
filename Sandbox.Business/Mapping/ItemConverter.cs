/*
using PROficiencyAdminEditor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROficiencyAdminEditor.Data.Mapping
{
    public class ItemConverter : EntityConverterBase<Entity.ItemEntity, ItemEntity>
    {
        readonly ItemDetailConverter _itemDetailConverter;
        readonly ItemDetailRuleExtensionConverter _itemDetailRuleExtensionConverter;
        readonly ItemDetailRuleNoteExtensionConverter _itemDetailRuleNoteExtensionConverter;

        public ItemConverter(
            ItemDetailConverter itemDetailConverter,
            ItemDetailRuleExtensionConverter itemDetailRuleExtensionConverter,
            ItemDetailRuleNoteExtensionConverter itemDetailRuleNoteExtionsionConverter
            )
        {
            _itemDetailConverter = itemDetailConverter;
            _itemDetailRuleExtensionConverter = itemDetailRuleExtensionConverter;
            _itemDetailRuleNoteExtensionConverter = itemDetailRuleNoteExtionsionConverter;
        }

        public override ItemEntity ToModel(Entity.ItemEntity entity)
        {
            var m = base.ToModel(entity);
            m.ItemDetailsList = [.. _itemDetailConverter.ToModels(entity.ItemDetails)];
            m.ItemDetailRuleExtensionList = [.. _itemDetailRuleExtensionConverter.ToModels(entity.ItemDetailRuleExtensions)];
            m.ItemDetailRuleNoteExtensionList = [.. _itemDetailRuleNoteExtensionConverter.ToModels(entity.ItemDetailRuleNoteExtensions)];
            return m;
        }

        public override IEnumerable<ItemEntity> ToModels(IEnumerable<Entity.ItemEntity> entities)
        {
            var mList = new List<ItemEntity>();
            foreach (var entity in entities) mList.Add(ToModel(entity));
            return mList;
        }
    }
}
*/
