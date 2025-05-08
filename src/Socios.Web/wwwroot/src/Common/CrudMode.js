export default class CrudMode {

    static get detailMode() { return '_detail_' }
    static get updateMode() { return '_update_' }
    static get createMode() { return '_create_' }
    static get deleteMode() { return '_delete_' }
    static get noneMode() { return '_none_' }

    constructor() {
        this.value = CrudMode.noneMode
    }

    get isDetail() { return this.value === CrudMode.detailMode }
    get isDelete() { return this.value === CrudMode.deleteMode }
    get isCreate() { return this.value === CrudMode.createMode }
    get isUpdate() { return this.value === CrudMode.updateMode }
    get isNone() { return this.value === CrudMode.noneMode }

    // indicates if the user can edit values
    get isEdit() { return this.value === CrudMode.updateMode || this.value === CrudMode.createMode }

    set(newMode) { this.value = newMode }
    setDetail() { this.value = CrudMode.detailMode; return this }
    setUpdate() { this.value = CrudMode.updateMode; return this }
    setDelete() { this.value = CrudMode.deleteMode; return this }
    setCreate() { this.value = CrudMode.createMode; return this }
    setNone() { this.value = CrudMode.noneMode; return this }

    static fromDetail() { return new CrudMode().setDetail() }
    static fromCreate() { return new CrudMode().setCreate() }
    static fromNone() { return new CrudMode().setNone() }
}