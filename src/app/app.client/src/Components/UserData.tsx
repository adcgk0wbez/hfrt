import { useEffect, useState } from 'react';

const UserData = () => {
    const [data, setData] = useState<ResponseDto | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            const response = await fetch('/user', { cache: 'no-store' });
            const data: ResponseDto = await response.json();
            setData(data);
        };

        fetchData();
    }, []);

    useEffect(() => {
        console.log(data)
    }, [data]);

    return (
        <div>
            <div style={{ display: 'flex'}} >

                <div style={{ marginRight: '25px' }}>
                    <h2>Ages Plus Twenty</h2>
                    {data?.ages?.map((age: AgePlusTwentyDto) => {
                        const user = data.users?.find((user: UserEntity) => user.id === age.userId);
                        return (
                            <div key={age.userId}>
                                <p>Name: {user?.firstName} {user?.lastName}</p>
                                <p>Original Age: {age.originalAge}</p>
                                <p>Age Plus Twenty: {age.agePlusTwenty}</p>
                                <p>DOB: {user.dob}</p>
                                <p>Favourite Colour: {user.favouriteColour}</p>
                                <p>Email: {user.email}</p>
                                <hr />
                            </div>
                        );
                    })}
                </div>

                <div>
                    <h2>Top Colours</h2>
                    {data?.topColours?.map((colour: TopColoursDto, index: number) => (
                        <div key={index}>
                            <p>Colour: {colour.colour}</p>
                            <p>Count: {colour.count}</p>
                            <hr />
                        </div>
                    ))}
                </div>

            </div>
        </div>
    );
};

export default UserData;