
export type SidebarItemProps = {
    platform: string;
    username: string;
}


export default function SidebarItem(props: SidebarItemProps) {
    return  (
        <div className="border rounded-full px-8 py-1">
            <h4 className="text-xl font-bold">{props.platform}</h4>
            <p>{props.username}</p>
        </div>
    )
}