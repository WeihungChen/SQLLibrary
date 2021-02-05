using SQLLibrary.Structure.Create;
using SQLLibrary.Structure.Drop;
using SQLLibrary.Data.Select;
using SQLLibrary.Data.Delete;
using SQLLibrary.Data.Update;
using SQLLibrary.Data.Insert;
using SQLLibrary.Data.Condition;
using System.Data;
using System.Threading;

namespace SQLLibrary
{
    public abstract class BaseSQL
    {
        protected Mutex mutex = new Mutex();
        
        internal abstract string  GetCreateCommand(CreateExpression expression);
        internal abstract string GetDropCommand(DropExpression expression);
        internal abstract string GetSelectCommand(SelectDataDefinition data);
        internal abstract string GetSelectCommand(BaseSelectExpression<IConjunction<IExecute>> expression);
        internal abstract string GetDeleteCommand(DeleteExpression expression);
        internal abstract string GetUpdateCommand(UpdateExpression expression);
        internal abstract string GetInsertCommand(InsertExpression expression);

        public abstract bool Execute(string command);
        public abstract bool Execute(string command, out DataTable dataTable);

        public ICreateOptions<IExecute> Create(string tableName)
        {
            return new CreateExpression(tableName, this);
        }

        public IDropOptions<IExecute> Drop(string tableName)
        {
            return new DropExpression(tableName, this);
        }

        public ISelectOptions<IExecute> Select(string tableName)
        {
            return new SelectExpression(tableName, this);
        }

        public IDeleteOptions<IExecute> Delete(string tableName)
        {
            return new DeleteExpression(tableName, this);
        }

        public IUpdateOptions<IExecute> Update(string tableName)
        {
            return new UpdateExpression(tableName, this);
        }

        public IInsertOptions<IExecute> Insert(string tableName)
        {
            return new InsertExpression(tableName, this);
        }
    }
}
