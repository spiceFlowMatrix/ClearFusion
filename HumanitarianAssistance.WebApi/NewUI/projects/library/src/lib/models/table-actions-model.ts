export interface TableActionsModel {

    items: {
        button?: { status?: boolean, text?: string },
        delete?: boolean,
        edit?: boolean,
        download?: boolean

    },
    subitems: {
        button?: { status?: boolean, text?: string },
        delete?: boolean,
        edit?: boolean,
        download?: boolean
    }
}
